namespace LinkedList
{
    using System;

    /// <summary>
    /// Generic, dynamic data structure which works with references.
    /// </summary>
    /// <typeparam name="TType">The data type you want to store in the list.</typeparam>
    public class LinkedList<TType>
    {
        /// <summary>
        /// Single element which holds its current value and the reference of the next element.
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Holds the reference of the next element.
            /// </summary>
            private Node next;
            /// <summary>
            /// Holds the value of the current element.
            /// </summary>
            private TType currValue;

            /// <summary>
            /// Gets and sets the next element.
            /// </summary>
            public Node Next
            {
                get { return this.next; }
                set { this.next = value; }
            }

            /// <summary>
            /// Gets and sets the current value.
            /// </summary>
            public TType Value
            {
                get { return this.currValue; }
                set { this.currValue = value; }
            }
        }

        /// <summary>
        /// The first element which holds the references to all the next elements. 
        /// </summary>
        private Node head;
        /// <summary>
        /// The number of elements which are currently in the list.
        /// </summary>
        private int count;

        /// <summary>
        /// Returns the number of elements which are currently in the list.
        /// </summary>
        public int Count
        {
            get { return this.count; }
        }

        /// <summary>
        /// Returns the element at the given index
        /// </summary>
        /// <param name="index">The index of the element you want to get.</param>
        /// <returns>The element at the given index.</returns>
        public TType this[int index]
        {
            get { return this.ElementAt(index); }
        }

        /// <summary>
        /// Adds an element in the end of the list.
        /// </summary>
        /// <param name="element">The element which you want to add.</param>
        public void Add(TType element)
        {
            if (head == null)
            {
                head = new Node();
                head.Next = null;
                head.Value = element;
            }
            else
            {
                Node toAdd = new Node();
                toAdd.Value = element;

                Node currNode = head;
                while (currNode.Next != null)
                {
                    currNode = currNode.Next;
                }

                currNode.Next = toAdd;
            }

            count++;
        }

        /// <summary>
        /// Adds as much as you want elements in the end of the list.
        /// </summary>
        /// <param name="items">The elements you want to add in the end of the list.</param>
        public void AddRange(params TType[] items)
        {
            foreach (TType item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Inserts a single element into an index.
        /// </summary>
        /// <param name="element">The element you want to insert</param>
        /// <param name="index">The index you want to insert the element into.</param>
        public void InsertAt(TType element, int index)
        {
            if (count != 0 && (index < 0 || index > count))
            {
                string message = string.Format
                    ("You tried to reach index [{0}] but the index of the last element is [{1}]", index, count - 1);
                throw new IndexOutOfRangeException(message);
            }

            if (head != null && index == 0)
            {
                Node toInsert = new Node();
                toInsert.Value = element;
                toInsert.Next = head;

                head = toInsert;
            }
            else if (head == null)
            {
                head = new Node();
                head.Value = element;
                head.Next = null;
            }
            else
            {
                Node toInsert = new Node();
                toInsert.Value = element;

                int indexCounter = 0;
                Node currNode = head;
                while (currNode.Next != null && indexCounter + 1 != index)
                {
                    currNode = currNode.Next;
                    indexCounter++;
                }

                toInsert.Next = currNode.Next;
                currNode.Next = toInsert;

            }

            count++;
        }

        /// <summary>
        /// Inserts as much as you want elements into the index you want.
        /// </summary>
        /// <param name="index">The index you want to insert the elements into.</param>
        /// <param name="items">The elements you want to insert.</param>
        public void InsertRange(int index, params TType[] items)
        {
            foreach (TType item in items)
            {
                InsertAt(item, index);
                index++;
            }
        }

        /// <summary>
        /// Removes the first occurency of the element.
        /// </summary>
        /// <param name="item">The element you want to remove.</param>
        public void Remove(TType item)
        {
            bool containsItem = false;

            Node currNode = head;
            Node prevNode = null;
            while (currNode.Next != null)
            {
                if (currNode.Value.Equals(item))
                {
                    containsItem = true;
                    break;
                }
                prevNode = currNode;
                currNode = currNode.Next;
            }

            if (!containsItem && currNode.Value.Equals(item))
            {
                containsItem = true;
            }

            if (!containsItem && head.Value.Equals(item))
            {
                head = null;
            }
            else if (!containsItem)
            {
                throw new InvalidOperationException($"Element [{item.ToString()}] does not exist in the list.");
            }
            else if (containsItem && head.Value.Equals(item))
            {
                head = currNode.Next;
            }
            else
            {
                prevNode.Next = currNode.Next;
                currNode = null;
            }

            count--;
        }

        /// <summary>
        /// Removes an element at a given index.
        /// </summary>
        /// <param name="index">The index of the element you want to remove.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                string message = string.Format
                    ("You tried to reach index [{0}] but the index of the last element is [{1}]", index, count - 1);
                throw new IndexOutOfRangeException(message);
            }

            bool indexIsFound = false;
            Node prevNode = null;
            Node currNode = head;
            int indexCounter = 0;
            while (currNode.Next != null)
            {
                if (index == indexCounter)
                {
                    indexIsFound = true;
                    break;
                }

                prevNode = currNode;
                currNode = currNode.Next;
                indexCounter++;
            }

            if (!indexIsFound && index == indexCounter)
            {
                indexIsFound = true;
            }

            if (indexIsFound && index == 0)
            {
                head = currNode.Next;
            }
            else if (indexIsFound)
            {
                prevNode.Next = currNode.Next;
                currNode = null;
            }

            count--;
        }

        /// <summary>
        /// Removes as much elements as you want at a given index.
        /// </summary>
        /// <param name="index">The index from which you want to start the removal.</param>
        /// <param name="count">The number of elements you want to remove.</param>
        public void RemoveRange(int index, int count)
        {
            for (int currRemoval = 0; currRemoval < count; currRemoval++)
            {
                RemoveAt(index);
            }
        }

        /// <summary>
        /// Returns the element at the given index.
        /// </summary>
        /// <param name="index">The index of the element you want to get.</param>
        /// <returns>The element at the given index.</returns>
        public TType ElementAt(int index)
        {
            if (index < 0 || index >= count)
            {
                string message = string.Format
                    ("You tried to reach index [{0}] but the index of the last element is [{1}]", index, count - 1);
                throw new IndexOutOfRangeException(message);
            }

            Node currNode = head;
            bool searchIsFinished = false;
            int indexCounter = 0;
            while (!searchIsFinished)
            {
                if (currNode == null)
                {
                    searchIsFinished = true;
                    break;
                }

                if (indexCounter == index)
                {
                    return currNode.Value;
                }

                currNode = currNode.Next;
                indexCounter++;
            }

            return currNode.Value;
        }

        /// <summary>
        /// Gets the index of the element you want.
        /// </summary>
        /// <param name="item">The element which index you want to know.</param>
        /// <returns>The index of the element.</returns>
        public int IndexOf(TType item)
        {
            bool itemExists = false;

            Node currNode = head;
            int indexCount = 0;
            while (currNode != null)
            {
                if (currNode.Value.Equals(item))
                {
                    itemExists = true;
                    break;
                }

                currNode = currNode.Next;
                indexCount++;
            }

            if (itemExists)
            {
                return indexCount;
            }
            else
            {
                throw new InvalidOperationException($"Item [{item.ToString()}] does not exist.");
            }
        }

        /// <summary>
        /// Gets all the elements which are currently in the list.
        /// </summary>
        /// <returns>Array holding all the elements.</returns>
        public TType[] GetAllElements()
        {
            TType[] toReturn = new TType[count];

            Node currNode = head;
            int indexOfArr = 0;
            while (currNode != null)
            {
                toReturn[indexOfArr] = currNode.Value;

                indexOfArr++;
                currNode = currNode.Next;
            }

            return toReturn;
        }

        /// <summary>
        /// Transfers all the elements to a string seperated by a comma and a single space.
        /// </summary>
        /// <returns>String holding all the elements.</returns>
        public override string ToString()
        {
            return string.Join(", ", GetAllElements());
        }
    }
}
