using System;
using System.Collections;
using System.Collections.Generic;

namespace Day11
{
	public partial class IntcodeMachine
	{
		internal class IntcodeMachineMemory : IList<long>
		{
			private long[] _memory;

			public IntcodeMachineMemory(long[] program)
			{
				if (program is null)
				{
					throw new ArgumentNullException(nameof(program));
				}
				_memory = new long[program.Length];
				program.CopyTo(_memory, 0);
			}

			public void ResetWith(long[] program)
			{
				program.CopyTo(_memory, 0);
				Array.Clear(_memory, program.Length, _memory.Length - program.Length);
			}

			public long this[int index]
			{
				get => index < _memory.Length ? _memory[index] : 0;
				set
				{
					if (index >= _memory.Length)
					{
						long[] extendedMemory = new long[index + 1];
						_memory.CopyTo(extendedMemory, 0);
						_memory = extendedMemory;
					}
					_memory[index] = value;
				}
			}

			public long this[long index]
			{
				get => this[(int)index];
				set => this[(int)index] = value;
			}

			public int Count => _memory.Length;

			public bool IsReadOnly => _memory.IsReadOnly;

			public void Add(long item)
			{
				((ICollection<long>)_memory).Add(item);
			}

			public void Clear()
			{
				Array.Clear(_memory, 0, _memory.Length);
			}

			public bool Contains(long item)
			{
				return ((ICollection<long>)_memory).Contains(item);
			}

			public void CopyTo(long[] array, int arrayIndex)
			{
				_memory.CopyTo(array, arrayIndex);
			}

			public IEnumerator<long> GetEnumerator()
			{
				return ((IEnumerable<long>)_memory).GetEnumerator();
			}

			public int IndexOf(long item)
			{
				return ((IList<long>)_memory).IndexOf(item);
			}

			public void Insert(int index, long item)
			{
				((IList<long>)_memory).Insert(index, item);
			}

			public bool Remove(long item)
			{
				return ((ICollection<long>)_memory).Remove(item);
			}

			public void RemoveAt(int index)
			{
				((IList<long>)_memory).RemoveAt(index);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return _memory.GetEnumerator();
			}
		}
	}
}
