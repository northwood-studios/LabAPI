using NorthwoodLib.Pools;
using System.Collections.Concurrent;

namespace LabApi.Features.Extensions;

/// <summary>
/// Pool for <see cref="PriorityQueue{T}"/>.
/// </summary>
/// <typeparam name="T">Type of the queue.</typeparam>
public sealed class PriorityQueuePool<T> : IPool<PriorityQueue<T>>
{
    /// <summary>
    /// Gets a shared <see cref="PriorityQueue{T}"/> instance.
    /// </summary>
    public static readonly PriorityQueuePool<T> Shared = new();

    private readonly ConcurrentQueue<PriorityQueue<T>> _pool = new();

    /// <summary>
    /// Gives a pooled <see cref="PriorityQueue{T}"/> or creates a new one if the pool is empty.
    /// </summary>
    /// <returns>A <see cref="PriorityQueue{T}"/> instance from the pool.</returns>
    public PriorityQueue<T> Rent()
    {
        return _pool.TryDequeue(out PriorityQueue<T> set) ? set : new PriorityQueue<T>();
    }

    /// <summary>
    /// Returns a <see cref="PriorityQueue{T}"/> to the pool for reuse.
    /// </summary>
    /// <param name="queue">The <see cref="PriorityQueue{T}"/> to return to the pool.</param>
    public void Return(PriorityQueue<T> queue)
    {
        queue.Clear();
        _pool.Enqueue(queue);
    }
}
