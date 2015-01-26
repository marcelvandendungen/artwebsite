using AOOR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Tests
{
    [TestClass]
    public class ListExtensionsTests
    {
        [TestMethod]
        public void PromoteEntryTest()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3, 4 };
            // Act
            list.PromoteEntry(e => e == 2);
            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(list, new List<int> { 2, 1, 3, 4 }));
        }

        [TestMethod]
        public void DoNotPromoteFirstEntryTest()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3, 4 };
            // Act
            list.PromoteEntry(e => e == 1);
            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(list, new List<int> { 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void DemoteEntryTest()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3, 4 };
            // Act
            list.DemoteEntry(e => e == 2);
            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(list, new List<int> { 1, 3, 2, 4 }));
        }

        [TestMethod]
        public void DoNotDemoteLastEntryTest()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3, 4 };
            // Act
            list.DemoteEntry(e => e == 4);
            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(list, new List<int> { 1, 2, 3, 4 }));
        }
    }
}
