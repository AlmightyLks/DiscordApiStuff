using System.Collections;

namespace DiscordApiStuff.Core.Caching
{
    internal sealed class Cache<T> : IEnumerable
    {
        private readonly T[] array;
        private int addIndex;
        internal Cache(int fixedSize)
        {
            array = new T[fixedSize];
            addIndex = 0;
        }

        internal T[] GetCache()
        {
            return array.Clone() as T[];
        }
        internal void Add(T element)
        {
            array[addIndex] = element;

            addIndex++;
            if (addIndex >= array.Length)
            {
                addIndex = 0;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return array.GetEnumerator();
        }
    }
}
