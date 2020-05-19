using System;

namespace KO.TBLCryptoEditor.Core
{
    public class CryptoKey
    {
        public long Offset { get; }
        public ushort Key { get; }
        public long VirtualAddress { get; }

        public CryptoKey(long offset, ushort key, long virtualAddress)
        {
            Offset = offset;
            Key = key;
            VirtualAddress = virtualAddress;
        }

        public override bool Equals(object obj)
        {
            CryptoKey key = (CryptoKey)obj;
            return key.Offset == Offset 
                && key.Key == Key 
                && key.VirtualAddress == VirtualAddress;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Offset.GetHashCode() * 397) ^ Key.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"[0x{(int)Offset:X8}||0x{(int)VirtualAddress:X8}]: 0x{Key:X4}";
        }
    }
}
