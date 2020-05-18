using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Workshell.PE;
using PatternFinder;

using KO.TBLCryptoEditor.Utils;

namespace KO.TBLCryptoEditor.Core
{
    public partial class TargetPE
    {
        private byte[] _data;

        public PortableExecutableImage PE { get; }
        public LocationCalculator Calculator { get; }
        public FileInfo FileInfo { get; }
        public string FilePath { get; }
        public int MajorLinkerVersion { get; private set; }
        public long BaseAddress { get; private set; }
        public CryptoPatchContainer CryptoPatches { get; private set; }
        public int ClientVersion { get; private set; }
        public bool IsValid { get; private set; }
        public bool CanUpdateKey2 { get; private set; }


        public TargetPE(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (fi.Exists)
            {
                _data = GetFileBytes(fi.FullName);
                if (_data == null)
                    throw new FileLoadException("The target file seem to be locked.");

                PE = PortableExecutableImage.FromBytes(_data);
                if (PE != null && PE.Is32Bit)
                {
                    Calculator = PE.GetCalculator();

                    FilePath = fi.FullName;
                    FileInfo = fi;

                    BaseAddress = (long)PE.NTHeaders.OptionalHeader.ImageBase;
                    MajorLinkerVersion = PE.NTHeaders.OptionalHeader.MajorLinkerVersion;

                    CryptoPatches = new CryptoPatchContainer();
                    ClientVersion = -1;

                    IsValid = true;
                    CanUpdateKey2 = true;
                }
            }
        }

        public InitializationFailure Initialize(bool skipKOValidation, bool skipClientVersionRetrieval)
        {
            if (_data == null)
                return new InitializationFailure("Failed to retrieve data from the target executeable.");

            if (!skipKOValidation && !ValidateKOClient())
                return new InitializationFailure("Failed to validate target as Knight Online executeable.");

            if (!skipClientVersionRetrieval && !InitClientVersion())
                return new InitializationFailure("Failed to retrieve target client version.");

            if (!InitKeysOffsets())
                return new InitializationFailure("Failed to retrieve all offsets for tbl keys.");

            return null;
        }

        private bool ValidateKOClient()
        {
            // Validates that we load "Knight OnLine Client" PE.
            var KnightOnLineClient = Pattern.Transform("4B 6E 69 67 68 74 20 4F 6E 4C 69 6E 65 20 43 6C 69 65 6E 74 00");
            long offset;
            if (!Pattern.Find(_data, KnightOnLineClient, out offset))
            {
                Console.WriteLine("[TargetPE::ValidateKOClient]: Doesn't seem like a valid KO exe.");
                return false;
            }

            return true;
        }

        private bool InitClientVersion()
        {
            const int MAX_STEPS_FORWARD = 4;
            const int MIN_CLIENT_VERSION = 1000;
            const int MAX_CLIENT_VERSION = 5000;

            var pattern = Pattern.Transform("68 C3 12 00 00");
            long offset;
            if (!Pattern.Find(_data, pattern, out offset))
            {
                Console.WriteLine("[TargetPE::InitClientVersion]: Failed to find resource offset.");
                return false;
            }

            MemoryStream ms = (MemoryStream)PE.GetStream();
            ms.Seek(offset, SeekOrigin.Begin);
            byte[] buff = new byte[4];
            ms.Read(buff, 0, 4, 1);
            int resourceId = BitConverter.ToInt32(buff, 0);
            if (resourceId != 4803)
            {
                Console.WriteLine($"[TargetPE::InitClientVersion]: Invalid resource id: {resourceId}." +
                                    $"Are we at the right location? :)");
                return false;
            }

            // https://c9x.me/x86/html/file_module_x86_id_35.html
            byte[] cmps = { 0x81, 0x3D };

            long originOffset = ms.Position;
            long stepsBack = 72;
            long startOffset = originOffset - stepsBack;
            foreach (byte cmp in cmps)
            {
                ms.Position = startOffset;

                // while it's not a compare instruction, keep reading byte bites ;)
                do
                {
                    ms.Read(buff, 0, 1);
                    if (ms.Position > originOffset)
                        return false;
                } while (buff[0] != cmp);

                for (int i = 0; i < MAX_STEPS_FORWARD; i++)
                {
                    ms.Read(buff, 0, 4);
                    int version = BitConverter.ToInt32(buff, 0);
                    if (version > MIN_CLIENT_VERSION && version < MAX_CLIENT_VERSION)
                    {
                        ClientVersion = version;
                        Console.WriteLine("[TargetPE::InitClientVersion]: Found client version v" + ClientVersion);
                        return true;
                    }
                    ms.Position -= 3;
                }
            }

            return false;
        }

        private bool InitKeysOffsets()
        {
            CryptoPatches.Clear();

            // "N3TableBase - Can't open file(read) File Handle..."
            var pattern = Pattern.Transform("4E 33 54 61 62 6C 65 42 61 73 65 20 2D 20 43"); // "N3TableBase - C"
            long offsetLogWrite;
            if (!Pattern.Find(_data, pattern, out offsetLogWrite))
            {
                Console.WriteLine("[TargetPE::InitKeysOffsets]: Unable to find offset.");
                return false;
            }

            // Generate pattern for finding all references to that string
            string logWriteVA = Calculator.OffsetToVA((ulong)offsetLogWrite).ToString("X8");
            string logWriteGenPattern = "68"; // push
            for (int i = logWriteVA.Length - 1; i >= 0; --i)
                logWriteGenPattern += i % 2 == 0 ? " " + logWriteVA.Substring(i, 2) : "";
            logWriteGenPattern += " E8"; // call

            Console.WriteLine($"[TargetPE::InitKeysOffsets]: Generated pattern result: {logWriteGenPattern}");
            var patternStrRefRegion = Pattern.Transform(logWriteGenPattern);
            List<long> regionStrRefOffsets;
            if (!Pattern.FindAll(_data, patternStrRefRegion, out regionStrRefOffsets))
            {
                Console.WriteLine("[TargetPE::InitKeysOffsets]: No offsets were found.");
                return false;
            }

            Console.WriteLine($"[TargetPE::InitKeysOffsets]: Found {regionStrRefOffsets.Count} region offsets.");

            var patDecryptRegions = new Pattern.Byte[][] {
                // push ?? <- DWORD as short
                // push ?? <- DWORD as short
                // push ?? <- DWORD as short
                Pattern.Transform("68 ?? ?? 00 00 68 ?? ?? 00 00 68 ?? ?? 00 00 ?? ?? E8"),

                //Pattern.Transform("FF ?? ?? ?? ?? ?? 8B ?? ?? ?? ?? ?? 55 FF"),
                //Pattern.Transform("FF ?? ?? ?? ?? ?? 8B ?? ?? ?? ?? ?? 56 FF"),
                //Pattern.Transform("FF ?? ?? ?? ?? ?? 8B ?? ?? ?? ?? ?? 57 FF"),

                // call ?? <- ReadFile
                // push ebp|esi|edi
                // call ?? <- CloseHandle
                // xor reg,reg
                Pattern.Transform("FF ?? ?? ?? ?? ?? 55 FF"),
                Pattern.Transform("FF ?? ?? ?? ?? ?? 56 FF"),
                Pattern.Transform("FF ?? ?? ?? ?? ?? 57 FF"),
            };

            const int ENCRYPT_REGION_MIN = 50;
            const int ENCRYPT_REGION_MAX = 700;
            const int ESTIMATED_DES_RANGE_BYTES = 280;

            bool detectedDesEncryption = false;
            foreach (long regionStrRefOffset in regionStrRefOffsets)
            {
                long decryptRegionOffset = -1;
                bool isInlinedFunction = false;
                for (int i = 0; i < patDecryptRegions.Length; i++)
                {
                    if (!Pattern.Find(_data, patDecryptRegions[i], out decryptRegionOffset, 
                                      regionStrRefOffset + ENCRYPT_REGION_MIN, regionStrRefOffset + ENCRYPT_REGION_MAX))
                        continue;

                    // Estimate average distance by bytes, to know whether there is more code for DES encryption before XOR.
                    if (!detectedDesEncryption && (decryptRegionOffset - regionStrRefOffset) > ESTIMATED_DES_RANGE_BYTES)
                        detectedDesEncryption = true;

                    // First pattern: we know it's a function that got inlined by the compiler.
                    // +5 for ptr & +2 for xor reg,reg
                    isInlinedFunction = i != 0;
                    if (isInlinedFunction)
                    {
                        CanUpdateKey2 = false;
                        decryptRegionOffset += patDecryptRegions[i].Length + 5 + 2;
                    }

                    break;
                }

                if (decryptRegionOffset == -1)
                {
                    Console.WriteLine($"[TargetPE::InitKeysOffsets]: Unable to find decryption region offset.");
                    continue;
                }

                CryptoKey[] keys = new CryptoKey[CryptoPatch.KEYS_COUNT];

                MemoryStream ms = (MemoryStream)PE.GetStream();
                ms.Seek(decryptRegionOffset, SeekOrigin.Begin);

                if (isInlinedFunction)
                {
                    if (!InitInlinedKeys(keys, ms))
                        return false;
                }
                else
                {
                    for (int i = CryptoPatch.KEYS_COUNT - 1; i >= 0; i--)
                    {
                        DWORD dword;
                        ms.Position++;
                        dword = ms.ReadStructure<DWORD>();
                        keys[i] = new CryptoKey((ms.Position - 4), (ushort)dword.sValue1,
                                                (long)Calculator.OffsetToVA((ulong)(ms.Position - 4)));
                    }
                }
                long keyVA = (long)Calculator.OffsetToVA((ulong)decryptRegionOffset);
                var patch = new CryptoPatch(decryptRegionOffset, keyVA, keys, isInlinedFunction);
                CryptoPatches.Add(patch);
            }

            // Update the container cryption type, so we know what we deal with
            CryptoPatches.CryptoType = detectedDesEncryption ? CryptoType.DES_XOR : CryptoType.XOR;

            if (CryptoPatches.Count != regionStrRefOffsets.Count)
                return false;

            if (!VerifyAllMatchedKeys(CryptoPatches))
                return false;

            return true;
        }

        private bool InitInlinedKeys(CryptoKey[] keys, MemoryStream ms)
        {
            // Key1 find routine
            var patKey1 = new Pattern.Byte[][] {
                        Pattern.Transform("B8 ?? ?? 00 00 7E"),
                        Pattern.Transform("BA ?? ?? 00 00 85"),
            };

            long key1Offset = -1;
            for (int keyIndex = 0; keyIndex < patKey1.Length; keyIndex++)
            {
                if (Pattern.Find(_data, patKey1[keyIndex], out key1Offset, ms.Position, ms.Position + 500))
                {
                    ms.Position = key1Offset + 1;
                    break;
                }
            }
            if (key1Offset == -1)
            {
                Console.WriteLine($"[TargetPE::InitInlinedKeys]: Unable to find key1.");
                return false;
            }
            DWORD dword = ms.ReadStructure<DWORD>();
            keys[0] = new CryptoKey((ms.Position - 4), (ushort)dword.sValue1,
                                    (long)Calculator.OffsetToVA((ulong)(ms.Position - 4)));

            // Key2 find routine
            // TODO(Gilad): Key got inlined by multiple asm instructions...Will need to implement algorithm to 
            // calculate from instructions region of what's the key value (TryGettingInlinedKey2Value()).
            keys[1] = new CryptoKey(-1, 0xFFFF, -1);

            // Key3 find routine
            var patKey2 = new Pattern.Byte[][] {
                        Pattern.Transform("8D 94 02 ?? ?? 00 00 7C"),
                        Pattern.Transform("B8 ?? ?? 00 00 8D"),
                        Pattern.Transform("B8 ?? ?? 00 00 2B"),
            };

            long key2Offset = -1;
            for (int keyIndex = 0; keyIndex < patKey2.Length; keyIndex++)
            {
                if (Pattern.Find(_data, patKey2[keyIndex], out key2Offset, ms.Position, ms.Position + 700))
                {
                    ms.Position = key2Offset + (keyIndex == 0 ? 3 : 1);
                    break;
                }
            }
            if (key2Offset == -1)
            {
                Console.WriteLine($"[TargetPE::InitInlinedKeys]: Unable to find key2.");
                return false;
            }
            dword = ms.ReadStructure<DWORD>();
            keys[2] = new CryptoKey((ms.Position - 4), (ushort)dword.sValue1,
                                    (long)Calculator.OffsetToVA((ulong)(ms.Position - 4)));

            return true;
        }

        private bool VerifyAllMatchedKeys(CryptoPatchContainer patches)
        {
            var samplePatch = CryptoPatches.Find(p => !p.Inlined);
            var inlinedPatches = CryptoPatches.Where(p => p.Inlined);
            foreach (var inlinedPatch in inlinedPatches)
            {
                if (inlinedPatch.Keys[0].Key != samplePatch.Keys[0].Key)
                    return false;

                if (inlinedPatch.Keys[2].Key != samplePatch.Keys[2].Key)
                    return false;
            }

            var regularPatches = CryptoPatches.Where(p => !p.Inlined);
            foreach (var patch in regularPatches)
            {
                for (int i = 0; i < CryptoPatch.KEYS_COUNT; i++)
                {
                    if (patch.Keys[i].Key != samplePatch.Keys[i].Key)
                        return false;
                }
            }

            return true;
        }

        public bool Patch(short key_r, short key_c1, short key_c2)
        {
            if (_data == null)
                return false;

            short[] newKeys = { key_r, key_c1, key_c2 };
            using (var ms = new MemoryStream(_data))
            {
                foreach (var patch in CryptoPatches)
                {
                    for (int i = CryptoPatch.KEYS_COUNT - 1; i >= 0; i--)
                    {
                        if (i == 1 && !CanUpdateKey2)
                            continue;

                        ms.Position = patch.Keys[i].Offset;
                        DWORD dword = (DWORD)newKeys[i];
                        ms.WriteByte(dword.byValue1);
                        ms.WriteByte(dword.byValue2);
                    }
                }

                if (FileInfo.Exists)
                    FileInfo.Delete();

                File.WriteAllBytes(FileInfo.FullName, _data);

                // Re-initialize new changes, to verify we patched everything correctly.
                if (!InitKeysOffsets())
                    return false;
            }

            return true;
        }

        public bool PatchInlinedFunctions(short key_r, short key_c1, short key_c2)
        {
            if (_data == null)
                return false;

            // TODO(Gilad): Implement.

            return true;
        }

        private byte[] GetFileBytes(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("[TargetPE::GetFileBytes]: File path cannot be null.");
            if (!File.Exists(filePath))
                throw new FileNotFoundException("[TargetPE::GetFileBytes]: The path provided seem to be invalid.");

            if (Utility.IsFileLocked(filePath))
                return null;

            return File.ReadAllBytes(filePath);
        }

        public bool CreateBackup(string destFilePath = "")
        {
            if (!IsValid)
                return false;

            if (String.IsNullOrEmpty(destFilePath))
                destFilePath = FilePath;

            int iBackupCount = 1;
            string dstFileTmp;
            do
            {
                dstFileTmp = $"{destFilePath}_bak{iBackupCount++}";
                if (!File.Exists(dstFileTmp))
                {
                    FileInfo.CopyTo(dstFileTmp);
                    return true;
                }
            } while (File.Exists(dstFileTmp));

            return false;
        }
    }
}
