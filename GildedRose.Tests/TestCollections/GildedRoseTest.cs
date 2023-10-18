using GildedRose.Entities;
using GildedRose.Repositories;
using GildedRose.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Tests.TestCollections
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void WhenPassingFakeDataSameNameShouldBePresent()
        {
            // Arrange
            IList<Item> fakeItems = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };

            var repositoryMock = new Mock<IItemReposity>();
            repositoryMock.Setup(x => x.GetItems()).Returns(() => fakeItems);

            var service = new ItemService(repositoryMock.Object);

            // Act
            service.UpdateQuality();
            var items = service.GetItems();

            // Assert
            Assert.AreEqual("foo", items.First().Name);
        }
    }
}
