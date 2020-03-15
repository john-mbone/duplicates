using DuplicatesDLL.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DuplicatesDLL.Algorithims
{
    //Method deprecated replcaed by LinkedListHelpers was a testing base.
    public class DoublyLinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        /*
       * [] private head and size vars
       * [] Node _firstNode ref to the first elem
       * [] Node _lastNode ref to the last elem
       * [] dataSet hashset to store single data item 
       * [*] add virtual empty checker from count Getter
       * [*] add count getter
       * [*] add method add object at last and first while checking count
       * [*] add method at position (_setElementAt((int index, T value)))
       * [*] get element method at pos
       * [*] get first element
       * [*] get last element
       * [*] get element at index
       * [*] DoublyLinkedList<T> indexer
       * [*] Human readable
       * [] remove at index only if count is greater than two otherwise preserve state
       * [] char selection sort if typeof T is char else ignore ,not for this question
       * [] remove at a position and re assign pointers
       */



        private int _count = 0;

        private const string DebugSymbol = "Duplicates Class=>";
        private DLinkedListNode<T> _firstNode { get; set; } = null;
        private DLinkedListNode<T> _lastNode { get; set; } = null;
        private HashSet<T> dataSet { get; set; }

        public DoublyLinkedList()
        {
            dataSet = new HashSet<T>();
        }

        public virtual HashSet<T> commonItems
        {
            get{ return dataSet; }
        }

        public virtual DLinkedListNode<T> root
        {
            get { return _firstNode; }
        }

        public virtual DLinkedListNode<T> Head
        {
            get { return this._firstNode; }
        }

        /// <summary>
        /// checks if size is empty or not by using count variable
        /// </summary>
        public virtual bool IsEmpty() => this.Count == 0;

        //get number of elements
        public virtual int Count
        {
            get { return this._count; }
            set { this._count = value; }
        }


        /// <summary>
        /// Sets the value of the element at the specified index
        /// </summary>
        /// <param name="index">Index of element to update.</param>
        /// <returns>Element</returns>
        protected virtual void _setElementAt(int index, T value)
        {
            if (IsEmpty() || index < 0 || index >= Count)
            {
                Debug.WriteLine($"Empty List : {index} => {value} not set.", DebugSymbol);
                return;
            }



            if (index == 0)
            {
                _firstNode.Data = value;//O(1)
            }
            else if (index == (Count - 1))
            {
                _lastNode.Data = value;//O(1)
            }
            else
            {
                // Decide from which reference to traverse the list, 
                //and then move the currentNode reference to the index
                // If index > half then traverse it from the end (_lastNode reference)
                // Otherwise, traverse it from the beginning (_firstNode refrence)
                //to run at O(1) ---traversing a node from the middle is O(1) we save time here

                DLinkedListNode<T> currentNode = null;

                if (index > (Count / 2))//_lastNode chooser
                {
                    currentNode = this._lastNode;

                    for (int i = 0; i < (Count - 1); ++i)
                    {
                        currentNode = currentNode.Previous;
                    }
                }
                else
                {
                    currentNode = this._firstNode;

                    for (int i = 0; i < index; ++i)
                    {
                        currentNode = currentNode.Next;
                    }
                }

                currentNode.Data = value;

            }
        }



        /// <summary>
        /// Gets the element at the specified index
        /// </summary>
        /// <param name="index">Index of element</param>
        /// <returns>Element</returns>
        public virtual T _getElementAt(int index)
        {
            if (IsEmpty() || index < 0 || index >= Count)
            {
                Debug.WriteLine($"Empty List : {index} not traced.", DebugSymbol);
            }

            if (index == 0)
            {
                return First;
            }

            if (index == (Count - 1))
            {
                return Last;
            }

            DLinkedListNode<T> currentNode = null;

            // Decide from which reference to traverse the list, and then move the currentNode reference to the index
            // If index > half then traverse it from the end (_lastNode reference)
            // Otherwise, traverse it from the beginning (_firstNode refrence)
            if (index > (Count / 2))
            {
                currentNode = this._lastNode;
                for (int i = (Count - 1); i > index; --i)
                {
                    currentNode = currentNode.Previous;
                }
            }
            else
            {
                currentNode = this._firstNode;
                for (int i = 0; i < index; ++i)
                {
                    currentNode = currentNode.Next;
                }
            }

            return currentNode.Data;
        }


        /// <summary>
        /// Getter function that returns the first element
        /// </summary>
        public virtual T First
        {
            get
            {
                if (IsEmpty())
                {
                    Debug.WriteLine($"Empty List : head is empty.", DebugSymbol);
                }
                return _firstNode.Data;////O(1)
            }
        }

        /// <summary>
        /// Getter function that returns the last element
        /// </summary>
        public virtual T Last
        {
            get
            {
                if (IsEmpty())
                {
                    Debug.WriteLine($"Empty List : head is empty.", DebugSymbol);
                }

                if (_lastNode == null)
                {
                    var currentNode = _firstNode;
                    while (currentNode.Next != null)
                    {
                        currentNode = currentNode.Next;//O(n)
                    }
                    _lastNode = currentNode;
                    return currentNode.Data;
                }

                return _lastNode.Data;
            }
        }




        /// <summary>
        /// Add Last the specified dataItem at the end of the list.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        public virtual void Prepend(T dataItem)
        {
            DLinkedListNode<T> newNode = new DLinkedListNode<T>(dataItem);

            if (_firstNode == null)
            {
                //Prev->First->Prev Circular Linked List
                _firstNode = _lastNode = newNode;
            }
            else
            {
                //we dont loop here we operate at o(1)
                var currentNode = _lastNode;
                currentNode.Next = newNode;
                newNode.Previous = currentNode;
                _lastNode = newNode;
            }

            if (!dataSet.Contains(dataItem))
            {
                dataSet.Add(dataItem);
            }
            // Increment the count.

            _count++;
        }

        /// <summary>
        /// add the specified dataItem at the beginning of the list.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        public virtual void Append(T dataItem)
        {
            DLinkedListNode<T> newNode = new DLinkedListNode<T>(dataItem);

            if (_firstNode == null)
            {
                _firstNode = _lastNode = newNode;
            }
            else
            {
                var currentNode = _firstNode;
                newNode.Next = currentNode;
                currentNode.Previous = newNode;
                _firstNode = newNode;
            }

            // Increment the count.

            if (!dataSet.Contains(dataItem))
            {
                dataSet.Add(dataItem);
            }

            _count++;
        }

        /// <summary>
        /// Inserts the dataItem after specified index.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        /// <param name="index">Index.</param>
        public virtual void InsertAfter(T dataItem, int index)
        {
            // Insert at previous index.
            InsertAt(dataItem, index - 1);
        }



        /// <summary>
        /// Inserts the dataItem at the specified index.
        /// </summary>
        /// <param name="dataItem">Data item.</param>
        /// <param name="index">Index.</param>
        public virtual void InsertAt(T dataItem, int index)
        {
            if (index < 0 || index > Count)
            {
                Debug.WriteLine("Index out of bound", DebugSymbol);
                return;
            }

            if (index == 0)
            {
                Append(dataItem);
            }
            else if (index == Count)
            {
                Prepend(dataItem);
            }
            else
            {
                DLinkedListNode<T> currentNode = this._firstNode;

                for (int i = 0; i < index - 1; ++i)
                {
                    currentNode = currentNode.Next;
                }


                DLinkedListNode<T> newNode = new DLinkedListNode<T>(dataItem);

                var prevNext = currentNode.Next;

                if (prevNext != null)
                {
                    currentNode.Next.Previous = newNode;
                }

                newNode.Next = prevNext;
                currentNode.Next = newNode;
                newNode.Previous = currentNode;

                _count++;
            }

            if (!dataSet.Contains(dataItem))
            {
                dataSet.Add(dataItem);
            }

        }

        /// <summary>
        /// Remove the specified dataItem.
        /// </summary>
        public virtual void Remove(T dataItem)
        {
            // Handle index out of bound errors
            if (IsEmpty())
            {
                Debug.WriteLine($"Empty List : {dataItem} not traced.", DebugSymbol);
                return;
            }

            if (_firstNode.Data.EqualsTo(dataItem))
            {
                _firstNode = _firstNode.Next;

                if (_firstNode != null)
                    _firstNode.Previous = null;
            }
            else if (_lastNode.Data.EqualsTo(dataItem))
            {
                _lastNode = _lastNode.Previous;

                if (_lastNode != null)
                    _lastNode.Next = null;
            }
            else
            {
                // Remove
                var currentNode = _firstNode;

                // Get currentNode to reference the element at the index.
                while (currentNode.Next != null)
                {
                    if (currentNode.Data.EqualsTo(dataItem))
                        break;

                    currentNode = currentNode.Next;
                }//end-while

                // Throw exception if item was not found
                if (!currentNode.Data.EqualsTo(dataItem))
                    throw new Exception("Item was not found!");

                // Remove element
                DLinkedListNode<T> newPrevious = currentNode.Previous;
                DLinkedListNode<T> newNext = currentNode.Next;

                if (newPrevious != null)
                    newPrevious.Next = newNext;

                if (newNext != null)
                    newNext.Previous = newPrevious;

                currentNode = newPrevious;
            }

            // Decrement count.
            _count--;
        }


        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <returns>True if removed successfully, false otherwise.</returns>
        /// <param name="index">Index of item.</param>
        public virtual void RemoveAt(int index)
        {
            // Handle index out of bound errors
            if (IsEmpty() || index < 0 || index >= Count)
            {
                Debug.WriteLine($"Empty List : {index} not traced.", DebugSymbol);
                return;
            }

            // Remove
            if (index == 0)
            {
                _firstNode = _firstNode.Next;

                if (_firstNode != null)
                    _firstNode.Previous = null;
            }
            else if (index == Count - 1)
            {
                _lastNode = _lastNode.Previous;

                if (_lastNode != null)
                    _lastNode.Next = null;
            }
            else
            {
                int i = 0;
                var currentNode = _firstNode;

                // Get currentNode to reference the element at the index.
                while (i < index)
                {
                    currentNode = currentNode.Next;
                    i++;
                }//end-while


                // Remove element
                var newPrevious = currentNode.Previous;
                var newNext = currentNode.Next;
                newPrevious.Next = newNext;

                if (newNext != null)
                    newNext.Previous = newPrevious;

                currentNode = newPrevious;
            }//end-else

            // Decrement count.
            _count--;
        }


        /// <summary>
        /// Tries to find a match for the predicate. Returns true if found; otherwise false.
        /// </summary>
        public virtual bool TryFindFirst(Predicate<T> match, out T found)
        {
            // Initialize the output parameter
            found = default(T);

            if (IsEmpty())
                return false;

            var currentNode = _firstNode;

            try
            {
                while (currentNode != null)
                {
                    if (match(currentNode.Data))
                    {
                        found = currentNode.Data;
                        return true;
                    }

                    currentNode = currentNode.Next;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Find the first element that matches the predicate from all elements in list.
        /// </summary>
        public virtual T FindFirst(Predicate<T> match)
        {
            if (IsEmpty())
            {
                Debug.WriteLine($"Empty List : {match.Target} not traced.", DebugSymbol);
            }

            var currentNode = _firstNode;

            while (currentNode != null)
            {
                if (match(currentNode.Data))
                    return currentNode.Data;

                currentNode = currentNode.Next;
            }

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Find all elements in list that match the predicate.
        /// </summary>
        /// <param name="match">Predicate function.</param>
        /// <returns>List of elements.</returns>
        public virtual List<T> FindAll(Predicate<T> match)
        {
            if (IsEmpty())
            {
                Debug.WriteLine($"Empty List : {match.Target} not traced.", DebugSymbol);
            }

            var currentNode = _firstNode;
            var list = new List<T>();

            while (currentNode != null)
            {
                if (match(currentNode.Data))
                    list.Add(currentNode.Data);

                currentNode = currentNode.Next;
            }

            return list;
        }

        /// <summary>
        /// Return an array version of this list.
        /// </summary>
        /// <returns></returns>
        public virtual T[] ToArray()
        {
            T[] array = new T[Count];

            var currentNode = _firstNode;
            for (int i = 0; i < Count; ++i)
            {
                if (currentNode != null)
                {
                    array[i] = currentNode.Data;
                    currentNode = currentNode.Next;
                }
                else
                {
                    break;
                }
            }

            return array;
        }

        /// <summary>
        /// Returns the index of an item if exists.
        /// </summary>
        public virtual int IndexOf(T dataItem)
        {
            int i = 0;
            bool found = false;
            var currentNode = _firstNode;

            // Get currentNode to reference the element at the index.
            while (i < Count)
            {
                if (currentNode.Data.EqualsTo(dataItem))
                {
                    found = true;
                    break;
                }

                currentNode = currentNode.Next;
                i++;
            }//end-while

            return (found == true ? i : -1);
        }

        /// <summary>
        /// Clears the list.
        /// </summary>
        public virtual void Clear()
        {
            _count = 0;
            _firstNode = _lastNode = null;
        }

        /// <summary>
        /// removes last match only if count of available or visited elements 
        /// is greater than 2,from question
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void RemoveMatch(T predicate)
        {
            if (IsEmpty())
            {
                Debug.WriteLine($"Empty List : {predicate} not traced.", DebugSymbol);
                return;
            }

            int match = 0;

            DLinkedListNode<T> currentNode = _firstNode;

            for (int i = 0; i < Count; i++)
            {
                if (currentNode.Data.EqualsTo(predicate))
                {
                    Debug.WriteLine(currentNode.Data, "predicate");

                    if (match > 1)
                    {
                        //remove item and decrease match
                        DLinkedListNode<T> newPrevious = currentNode.Previous;

                        DLinkedListNode<T> newNext = currentNode.Next;

                        if (newPrevious != null)
                            newPrevious.Next = newNext;

                        if (newNext != null)
                            newNext.Previous = newPrevious;

                        currentNode = newPrevious;

                        --match;
                        --Count;
                    }
                    else
                    {
                        ++match;
                    }
                }
                if (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                else
                {
                    break;
                }
            }

            if (!WithinLimit(predicate))
            {
                RemoveMatch(predicate);
            }

        }

        private bool WithinLimit(T value)
        {
            var keyValuePair = GetCount(value).First();

            var num = keyValuePair.Key;

            var firstPos = keyValuePair.Value;

            return (num <= 2);
        }


        /// <summary>
        /// Remove the specified dataItem.
        /// </summary>
        public virtual void RemoveFirstMatch(Predicate<T> predicate)
        {
            // Handle index out of bound errors
            if (IsEmpty())
            {
                Debug.WriteLine($"Empty List : {predicate.Target} not traced.", DebugSymbol);
                return;
            }

            if (predicate(_firstNode.Data))
            {
                _firstNode = _firstNode.Next;

                if (_firstNode != null)
                    _firstNode.Previous = null;
            }
            else if (predicate(_lastNode.Data))
            {
                _lastNode = _lastNode.Previous;

                if (_lastNode != null)
                    _lastNode.Next = null;
            }
            else
            {
                // Remove
                var currentNode = _firstNode;

                //Get currentNode to reference the element at the index.
                while (currentNode.Next != null)
                {

                    if (predicate(currentNode.Data))
                        break;

                    currentNode = currentNode.Next;
                }
                //end-while

                // If we reached the last node and item was not found
                // Throw exception
                if (!predicate(currentNode.Data))
                    throw new Exception("Item was not found!");

                // Remove element
                DLinkedListNode<T> newPrevious = currentNode.Previous;
                DLinkedListNode<T> newNext = currentNode.Next;

                if (newPrevious != null)
                    newPrevious.Next = newNext;

                if (newNext != null)
                    newNext.Previous = newPrevious;

                currentNode = newPrevious;
            }

            // Decrement count.
            _count--;
        }

        /// <summary>
        /// removes duplicates in un sorted liked list
        /// </summary>
        public virtual void removeDuplicates()
        {
            for (int i = 0; i < dataSet.Count; i++)
            {
                T elem = dataSet.ElementAt(i);
                RemoveMatch(elem);
            }
        }



        /// <summary>
        /// expensive process,we loop through every node 
        /// get count of dataItem and last index
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public virtual Dictionary<int, int> GetCount(T dataItem)
        {
            if (dataItem == null) throw new NullReferenceException($"Expected item of type {typeof(T)},null given");

            if (IsEmpty())
            {
                Debug.WriteLine($"Empty List : {dataItem} not traced.", DebugSymbol);
            }
            int counted = 0, pos = 0, first = 0;

            var found = new Dictionary<int, int>();
            var currentNode = _firstNode;


            while (currentNode.Data != null)
            {
                if (currentNode.Data.EqualsTo(dataItem))
                {
                    ++counted;
                    if (first == 0)
                    {
                        first = pos;
                    }
                }

                if (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                    ++pos;
                }
                else
                {
                    break;
                }
            }

            found.Add(counted, first);

            return found;
        }


        /// <summary>
        /// Returns the list items as a readable multi--line string.
        /// </summary>
        /// <returns></returns>
        public virtual string HumanReadable()
        {
            string listAsString = string.Empty;
            var currentNode = _firstNode;

            int i = 0;

            while (currentNode != null)
            {
                if (i == 0)
                {
                    listAsString = String.Format("{0}[{1}]{2}", listAsString, i, currentNode.Data);
                }
                else
                {
                    listAsString = String.Format("{0}->[{1}]{2}", listAsString, i, currentNode.Data);
                }
                currentNode = currentNode.Next;
                ++i;
            }

            return listAsString;
        }

        /// <summary>
        /// Implements the collection-index operator.
        /// Gets or sets the element at the specified index
        /// </summary>
        /// <param name="index">Index of element.</param>
        public virtual T this[int index]
        {
            get { return this._getElementAt(index); }
            set { this._setElementAt(index, value); }
        }


        public IEnumerator<T> GetEnumerator()
        {
            var node = _firstNode;
            while (node != null)
            {
                yield return node.Data;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



    }
}
