using DuplicatesDLL.Algorithims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace DLLTest
{
    [TestClass]
    public class DuplicatesTestClass
    {
        [Fact]
        public void Should_Append_To_Empty_List()
        {
            var list = new DoublyLinkedList<char>();

            list.Append('A');

            Assert.Equals(1, list.Count);
        }
    }
}
