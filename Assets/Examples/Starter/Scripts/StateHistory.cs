using System.Collections.Generic;

namespace Playgama.Examples.Starter.Scripts
{
    /// <summary>
    /// Represents a history of states with a fixed maximum size. This class is a generic FIFO (First-In-First-Out)
    /// queue that only retains a limited number of the most recent items.
    /// </summary>
    /// <typeparam name="T">
    /// The type of items to store in the state history.
    /// </typeparam>
    public class StateHistory<T> : Queue<T>
    {
        private readonly int _maxSize;

        public StateHistory(int maxSize)
        {
            _maxSize = maxSize;
        }

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            while (Count > _maxSize)
                Dequeue();
        }

        public override string ToString()
        {
            return string.Join(" → ", this);
        }
    }

}