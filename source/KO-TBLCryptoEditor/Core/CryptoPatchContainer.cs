using System;
using System.Collections;
using System.Collections.Generic;

namespace KO.TBLCryptoEditor.Core
{
    public class CryptoPatchContainer : IEnumerable<CryptoPatch>, ICollection<CryptoPatch>
    {
        public CryptoType CryptoType { get; set; }
        public bool Inlined { get; set; }
        public int Count => _patches.Count;
        public int Capacity 
        { 
            get => _patches.Count; 
            set => _patches.Capacity = value; 
        }
        public bool IsReadOnly => false;

        private List<CryptoPatch> _patches;

        public CryptoPatchContainer()
        {
            CryptoType = CryptoType.UNKNOWN;
            Inlined = false;
            _patches = new List<CryptoPatch>();
        }

        public CryptoPatch this[int index]
        {
            get
            {
                if (index >= _patches.Count)
                    throw new IndexOutOfRangeException();

                return _patches[index];
            }
            set
            {
                if (index >= _patches.Count)
                    throw new IndexOutOfRangeException();

                _patches[index] = value ?? throw new ArgumentNullException();
            }
        }

        public void Add(CryptoPatch patch)
        {
            if (patch == null)
                throw new ArgumentNullException();

            _patches.Add(patch);
        }

        public bool Remove(CryptoPatch patch)
        {
            if (patch == null)
                throw new ArgumentNullException();

            return _patches.Remove(patch);
        }

        public CryptoPatch Find(CryptoPatch patch)
        {
            if (patch == null)
                throw new ArgumentNullException();

            return _patches.Find(p => p == patch);
        }

        public CryptoPatch Find(Predicate<CryptoPatch> match)
        {
            if (match == null)
                throw new ArgumentNullException();

            for (int i = 0; i < Count; i++)
                if (match(_patches[i]))
                    return _patches[i];

            return default;
        }

        public List<CryptoPatch> FindAll(Predicate<CryptoPatch> match)
        {
            if (match == null)
                throw new ArgumentNullException();

            List<CryptoPatch> patches = new List<CryptoPatch>();
            for (int i = 0; i < Count; i++)
                if (match(_patches[i]))
                    patches.Add(_patches[i]);

            return patches;
        }

        public IEnumerator<CryptoPatch> GetEnumerator()
        {
            foreach (var patch in _patches)
                yield return patch;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _patches.Clear();
        }

        public bool Contains(CryptoPatch patch)
        {
            return _patches.Contains(patch);
        }

        public void CopyTo(CryptoPatch[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            CryptoPatch[] ppArray = array as CryptoPatch[];
            if (ppArray == null)
                throw new ArgumentException();

            ((ICollection<CryptoPatch>)this).CopyTo(ppArray, arrayIndex);
        }
    }
}
