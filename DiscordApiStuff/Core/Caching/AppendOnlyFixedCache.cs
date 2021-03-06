using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DiscordApiStuff.Core.Caching
{
    public partial struct AppendOnlyFixedCache<T>
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
    
    public partial struct AppendOnlyFixedCache<T>
    {
        private readonly T[] _arr;

        private int _readPos;
        public AppendOnlyFixedCache(int size)
        {
            //We are assuming this collection is long-lived
            _arr = GC.AllocateArray<T>(size);
            _readPos = -1;
        }

        public ref T this[int Index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_arr), Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            Add(ref item);
        }
        
        public void Add(ref T item)
        {
            var writePos = unchecked(++_readPos);
            var arr = _arr;
            if ((uint) writePos >= (uint) arr.Length)
            {
                arr[_readPos = 0] = item;
                return;
            }
            arr[writePos] = item;
        }
    }
}
