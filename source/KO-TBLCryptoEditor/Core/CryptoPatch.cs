using System;
using System.Collections;
using System.Collections.Generic;

namespace KO.TBLCryptoEditor.Core
{
    public class CryptoPatch : IEnumerable<CryptoKey>
    {
        public const int KEYS_COUNT = 3;

        public long RegionOffset { get; }
        public long RegionVA { get; }
        public CryptoKey[] Keys { get; set; }
        public bool Inlined { get; set; }


        public CryptoPatch(long regionOffset, long regionVA, CryptoKey[] keys = null, bool inlined = false)
        {
            if (keys != null && keys.Length != KEYS_COUNT)
                throw new ArgumentException("Mismatched keys length.");

            RegionOffset = regionOffset;
            RegionVA = regionVA;
            Keys = keys ?? new CryptoKey[KEYS_COUNT];
            Inlined = inlined;
        }

        public CryptoKey this[int index]
        {
            get
            {
                if (index > KEYS_COUNT - 1)
                    throw new IndexOutOfRangeException();

                return Keys[index];
            }
            set
            {
                if (index > KEYS_COUNT - 1)
                    throw new IndexOutOfRangeException();

                Keys[index] = value ?? throw new ArgumentNullException();
            }
        }

        public IEnumerator<CryptoKey> GetEnumerator()
        {
            foreach (var key in Keys)
                yield return key;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (RegionOffset.GetHashCode() * 397) ^ RegionVA.GetHashCode();
            }
        }
    }
}
