using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode.Year2019.Day18;

public class PriorityQueue<TElement, TPriority> where TPriority : notnull, IComparable<TPriority>
{
	private readonly SortedDictionary<TPriority, Queue<TElement>> _queue;
	private readonly Func<TElement, TPriority> _prioritySelector;

	public int Count { get; private set; }

	public PriorityQueue(Func<TElement, TPriority> prioritySelector)
	{
		_prioritySelector = prioritySelector;
		_queue = new SortedDictionary<TPriority, Queue<TElement>>();
		Count = 0;
	}

	public void Enqueue(TElement element)
	{
		TPriority priority = _prioritySelector.Invoke(element);
		if (!_queue.TryGetValue(priority, out Queue<TElement>? subqueue))
		{
			subqueue = new Queue<TElement>();
			_queue.Add(priority, subqueue);
		}
		subqueue.Enqueue(element);
		++Count;
	}

	public bool TryDequeue([NotNullWhen(true)] out TElement? element)
	{
		if (Count == 0)
		{
			element = default;
			return false;
		}
		(TPriority priority, Queue<TElement> subqueue) = _queue.First();
		element = subqueue.Dequeue()!;
		if (subqueue.Count == 0)
		{
			_queue.Remove(priority);
		}
		--Count;
		return true;
	}
}
