using System;
using System.Collections.Generic;

namespace LabApi.Features.Extensions;

/// <summary>
/// Priority queue class.
/// </summary>
/// <typeparam name="T">Data type to store in the queue.</typeparam>
public class PriorityQueue<T>
{
    /// <summary>
    /// Gets the number of items currently in the queue.
    /// </summary>
    public int Count => _elements.Count;

    private readonly List<ValueTuple<int, T>> _elements;

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
    /// </summary>
    public PriorityQueue()
    {
        _elements = new List<ValueTuple<int, T>>();
    }

    /// <summary>
    /// Adds an item to the priority queue with the specified priority.
    /// Smaller numbers indicate higher priority.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <param name="priority">The priority of the item.</param>
    public void Enqueue(T item, int priority)
    {
        ValueTuple<int, T> newItem = new ValueTuple<int, T>(priority, item);
        _elements.Add(newItem);
        HeapifyUp(_elements.Count - 1);
    }
    /// <summary>
    /// Removes and returns the item with the highest priority (smallest priority value) from the queue.
    /// </summary>
    /// <returns>The item with the highest priority.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("The queue is empty.");

        T bestItem = _elements[0].Item2;
        int lastIndex = _elements.Count - 1;

        _elements[0] = _elements[lastIndex];
        _elements.RemoveAt(lastIndex);

        if (Count > 0)
            HeapifyDown(0);

        return bestItem;
    }
    /// <summary>
    /// Clears all items from the queue.
    /// </summary>
    public void Clear()
    {
        _elements.Clear();
    }

    private void HeapifyUp(int index)
    {
        int parentIndex = (index - 1) / 2;
        while (index > 0 && _elements[index].Item1 < _elements[parentIndex].Item1)
        {
            Swap(index, parentIndex);
            index = parentIndex;
            parentIndex = (index - 1) / 2;
        }
    }

    private void HeapifyDown(int index)
    {
        int leftChildIndex;
        int rightChildIndex;
        int smallestChildIndex;
        while (true)
        {
            leftChildIndex = (2 * index) + 1;
            rightChildIndex = (2 * index) + 2;
            smallestChildIndex = index;

            if (leftChildIndex < Count && _elements[leftChildIndex].Item1 < _elements[smallestChildIndex].Item1)
                smallestChildIndex = leftChildIndex;

            if (rightChildIndex < Count && _elements[rightChildIndex].Item1 < _elements[smallestChildIndex].Item1)
                smallestChildIndex = rightChildIndex;

            if (smallestChildIndex == index)
                break;

            Swap(index, smallestChildIndex);
            index = smallestChildIndex;
        }
    }

    private void Swap(int index1, int index2)
    {
        (_elements[index1], _elements[index2]) = (_elements[index2], _elements[index1]);
    }
}