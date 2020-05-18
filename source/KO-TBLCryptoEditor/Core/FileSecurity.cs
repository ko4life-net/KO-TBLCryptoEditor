using System;

namespace KO.TBLCryptoEditor.Core
{
    /// <summary>
    ///   Used for batch table conversion, so we know what kind of encryption the target uses.
    ///   DES won't be supported for now, to reduce impact on the official game, so cheaters 
    ///   won't copy this code to their bots for w/e reason.
    /// </summary>
    public enum CryptoType
    {
        /// <summary>
        ///   Simple XOR cipher encryption.
        /// </summary>
        XOR,

        /// <summary>
        ///   DES comes with some salt ;)
        ///   and then regular XOR cipher.
        /// </summary>
        DES_XOR,

        /// <summary>
        ///   :)
        /// </summary>
        UNKNOWN
    }

    public class FileSecurity
    {
        public static void EncryptXOR(byte[] data, short key_r, short key_c1, short key_c2)
        {
            for (int i = 0; i < data.Length; i++)
            {
                byte byData = (byte)(data[i] ^ (key_r >> 8));
                key_r = (short)((byData + key_r) * key_c1 + key_c2);
                data[i] = byData;
            }
        }

        public static void DecryptXOR(byte[] data, short key_r, short key_c1, short key_c2)
        {
            for (int i = 0; i < data.Length; i++)
            {
                byte byData = (byte)(data[i] ^ (key_r >> 8));
                key_r = (short)((data[i] + key_r) * key_c1 + key_c2);
                data[i] = byData;
            }
        }
    }
}
