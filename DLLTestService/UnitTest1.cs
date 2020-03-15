using DuplicatesDLL.Algorithims;
using DuplicatesDLL.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace DLLTestService
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Append_To_Empty_List()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Append('A');
            Assert.Equal('A', LinkedListHelpers<char>._getElementAt(0));
            Assert.Equal(1, LinkedListHelpers<char>.Count);

            LinkedListHelpers<char>.RemoveAt(0);
            Assert.Equal(0, LinkedListHelpers<char>.Count);
        }

        [Fact]
        public void Should_Append_To_NonEmpty_List()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Append('A');
            LinkedListHelpers<char>.Append('B');

            Assert.Equal(2, LinkedListHelpers<char>.Count);

            Assert.NotEqual(new char[] { 'A', 'B' }, LinkedListHelpers<char>.ToArray());
            Assert.Equal(new char[] { 'B', 'A' }, LinkedListHelpers<char>.ToArray());
        }

        [Fact]
        public void Should_Delete_Element_From_Single_Element_List()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Append('A');
            LinkedListHelpers<char>.Remove('A');

            Assert.Equal(0, LinkedListHelpers<char>.Count);
            Assert.Equal(new char[] { }, LinkedListHelpers<char>.ToArray());//empty
        }

        [Fact]
        public void Should_Delete_Element_From_Single_Element_List_With_Index()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Append('A');
            LinkedListHelpers<char>.RemoveAt(0);

            Assert.Equal(0, LinkedListHelpers<char>.Count);
            Assert.Equal(new char[] { }, LinkedListHelpers<char>.ToArray());//empty
        }


        [Fact]
        public void Should_Delete_Element_At_The_End_Typeof_Int()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Append('A');//head.next.next
            LinkedListHelpers<char>.Append('B');//head.next
            LinkedListHelpers<char>.Append('C');//head

            LinkedListHelpers<char>.RemoveAt(2);

            Assert.Equal(2, LinkedListHelpers<char>.Count);
            Assert.Equal(new char[] { 'C', 'B' }, LinkedListHelpers<char>.ToArray());
        }

        [Fact]
        public void Should_Delete_Element_At_The_End_Typeof_Prepend()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Prepend('A');//head
            LinkedListHelpers<char>.Prepend('B');//head.next
            LinkedListHelpers<char>.Prepend('C');//head.next.next

            LinkedListHelpers<char>.RemoveAt(2);

            Assert.Equal(2, LinkedListHelpers<char>.Count);

            Assert.Equal(new char[] { 'A', 'B' }, LinkedListHelpers<char>.ToArray());
        }

        [Fact]
        public void Should_Delete_Element_At_The_Begining_Typeof_Prepend()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Prepend('A');//head
            LinkedListHelpers<char>.Prepend('C');//head.next
            LinkedListHelpers<char>.Prepend('Z');//head.next.next
            LinkedListHelpers<char>.Prepend('Z');//head.next.next.next
            LinkedListHelpers<char>.Prepend('A');//head.next.next.next.next
            LinkedListHelpers<char>.Prepend('C');//head.next.next.next.next.next

            LinkedListHelpers<char>.RemoveAt(0);

            Assert.Equal(5, LinkedListHelpers<char>.Count);
            Assert.Equal(new char[] { 'C', 'Z', 'Z', 'A', 'C' }, LinkedListHelpers<char>.ToArray());
        }

        [Fact]
        public void Should_Delete_Element_At_First_Occurence_Typeof_Prepend()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Prepend('A');//head
            LinkedListHelpers<char>.Prepend('C');//head.next
            LinkedListHelpers<char>.Prepend('Z');//head.next.next
            LinkedListHelpers<char>.Prepend('Z');//head.next.next.next
            LinkedListHelpers<char>.Prepend('A');//head.next.next.next.next
            LinkedListHelpers<char>.Prepend('C');//head.next.next.next.next.next

            LinkedListHelpers<char>.Remove('Z');

            Assert.Equal(5, LinkedListHelpers<char>.Count);
            Assert.Equal(new char[] { 'A', 'C', 'Z', 'A', 'C' }, LinkedListHelpers<char>.ToArray());

            LinkedListHelpers<char>.Remove('A');
            Assert.Equal(4, LinkedListHelpers<char>.Count);

            Assert.Equal(new char[] { 'C', 'Z', 'A', 'C' }, LinkedListHelpers<char>.ToArray());
        }

        [Fact]
        public void Should_Not_Throw_When_Deleting_From_Empty_List_After_Appending_Some_Data()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Append('A');
            LinkedListHelpers<char>.Remove('A');
            LinkedListHelpers<char>.Remove('A');

            Assert.Equal(0, LinkedListHelpers<char>.Count);

            Assert.Equal(new char[] { }, LinkedListHelpers<char>.ToArray());
        }

        [Fact]
        public void Should_Not_Throw_When_Appending_At_Empty_Position()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.InsertAt('B', 10);

            Assert.Equal(0, LinkedListHelpers<char>.Count);
        }

        //"[0]A->[1]C->[2]Z->[3]Z->[4]A->[5]C"


        [Fact]
        public void Should_Not_Find_First()
        {
            LinkedListHelpers<char>.Clear();
            Predicate<char> prediction = p => p == 'A';

            char ch = 'A';

            Assert.False(LinkedListHelpers<char>.TryFindFirst(prediction, out ch));

            Assert.Equal(0, LinkedListHelpers<char>.Count);
        }

        [Fact]
        public void Should_Find_First_Assign_CH_A_()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Prepend('A');

            Predicate<char> prediction = p => p == 'A';

            char ch = '0';

            Assert.True(LinkedListHelpers<char>.TryFindFirst(prediction, out ch));

            Assert.Equal(1, LinkedListHelpers<char>.Count);

            Assert.Equal('A', ch);
        }

        [Fact]
        public void Should_Remove_Duplicates()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Append('B');
            LinkedListHelpers<char>.Append('A');
            LinkedListHelpers<char>.Append('B');
            LinkedListHelpers<char>.Append('E');
            LinkedListHelpers<char>.Append('E');
            LinkedListHelpers<char>.Append('B');
            LinkedListHelpers<char>.Append('E');

            LinkedListHelpers<char>.removeDuplicates();//removes duplicates leaving two if any

            Assert.Equal(5, LinkedListHelpers<char>.Count);
            Assert.Equal(new char[] { 'E', 'B', 'E', 'B', 'A' }, LinkedListHelpers<char>.ToArray());
        }

        [Fact]
        public void Should_Do_Nothing_No_Duplicates_Greater_Than_3()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Append('A');
            LinkedListHelpers<char>.Append('B');
            LinkedListHelpers<char>.Append('E');
            LinkedListHelpers<char>.Append('B');
            LinkedListHelpers<char>.Append('E');

            LinkedListHelpers<char>.removeDuplicates();//removes duplicates leaving two if any

            Assert.Equal(5, LinkedListHelpers<char>.Count);
            Assert.Equal(new char[] { 'E', 'B', 'E', 'B', 'A' }, LinkedListHelpers<char>.ToArray());
        }

        /// <summary>
        /// must be of type before value type.
        /// if node A = typeof(char) 
        /// next node B must equalTo typeof(A)
        /// else ignore user input.
        /// </summary>
        /// 
        [Fact]
        public void Should_Not_Add_Elements_of_Type_Int_or_Any__To_Char()
        {
            LinkedListHelpers<char>.Clear();
            LinkedListHelpers<char>.Append('A');
            LinkedListHelpers<char>.Append('B');
            LinkedListHelpers<char>.Append('E');
            LinkedListHelpers<char>.Append('B');
            LinkedListHelpers<char>.Append('E');

            LinkedListHelpers<int>.Append(1);//this is not added

            LinkedListHelpers<char>.removeDuplicates();//removes duplicates leaving two if any

            Assert.Equal(5, LinkedListHelpers<char>.Count);
            Assert.Equal(new char[] { 'E', 'B', 'E', 'B', 'A' }, LinkedListHelpers<char>.ToArray());
        }



    }
}
