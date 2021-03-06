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
        public AppendOnlyFixedCache(int Size)
        {
            //We are assuming this collection is long-lived
            _arr = GC.AllocateArray<T>(Size);
            _readPos = -1;
        }

        public ref T this[int Index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_arr), Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T Item)
        {
            Add(ref Item);
        }
        
        public void Add(ref T Item)
        {
            var writePos = unchecked(++_readPos);
            var arr = _arr;
            if ((uint) writePos >= (uint) arr.Length)
            {
                arr[_readPos = 0] = Item;
                return;
            }
            arr[writePos] = Item;
        }
    }
}
