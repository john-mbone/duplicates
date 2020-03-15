using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DuplicatesDLL.Algorithims
{
    public class DLinkedListNode<T> : IComparable<DLinkedListNode<T>> where T : IComparable<T>
    {
        /*
         * [*] data variable
         * [*] _next node
         * [*] _prev node
         * [*] _constructor one param for data
         * [*] _constructor three params _next prev and data takes nulls
         * [*] _constructor three params _next prev and data 
         * [*] Data setter and getter
         * [*] Next accessors and setter
         * [*] Prev accessors and setter
         */
        private T _data;

        private DLinkedListNode<T> _next;

        private DLinkedListNode<T> _previous;

        public DLinkedListNode() : this(default(T)) { }
        public DLinkedListNode(T dataItem) : this (dataItem,null,null){}

        public DLinkedListNode(T dataItem, DLinkedListNode<T> _next, DLinkedListNode<T> _previous)
        {
            this._data = dataItem;
            this._next = _next;
            this._previous = _previous;
        }

        public virtual T Data
        {
            get { return this._data; }
            set { this._data = value; }
        }

        public virtual DLinkedListNode<T> Next
        {
            get { return this._next; }
            set { this._next = value; }
        }

        public virtual DLinkedListNode<T> Previous
        {
            get { return this._previous; }
            set { this._previous = value; }
        }

        public int CompareTo([AllowNull] DLinkedListNode<T> other)
        {
            return (other == null ? -1 : Data.CompareTo(other.Data));
        }
    }
}
