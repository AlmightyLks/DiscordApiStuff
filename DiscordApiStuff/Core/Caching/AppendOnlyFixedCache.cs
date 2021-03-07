using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DiscordApiStuff.Core.Caching
{
    public partial class AppendOnlyFixedCache<T>
    {
        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _arr.Length;
        }

        public T[] Array
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _arr;
        }

        public IEnumerator GetEnumerator()
        {
            return _arr.GetEnumerator();
        }
    }
    
    public partial class AppendOnlyFixedCache<T>
    {
        private readonly T[] _arr;
        private int _writePos;

        public AppendOnlyFixedCache(int size)
        {
            //We are assuming this collection is long-lived
            _arr = GC.AllocateArray<T>(size);
            _writePos = -1;
        }

        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_arr), index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            Add(ref item);
        }

        public void Add(ref T item)
        {
            //Unchecked blocks, uint comparisons & using a local var of the underlying array go through less checks
            //Faster by ~60%
            unchecked
            {
                int writePos = ++_writePos;
                T[] array = Array;
                if ((uint)writePos >= (uint)array.Length)
                {
                    _writePos = 0;
                }
                array[_writePos] = item;
            }
        }
    }
}
