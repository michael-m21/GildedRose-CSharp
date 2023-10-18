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
    public class ItemServiceTests
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

        [Test]
        public void NormalItemQualityShouldNeverBeNegative()
        {
            // Arrange
            IList<Item> fakeItems = new List<Item> { new Item { Name = "foo", SellIn = 1, Quality = 0, IsNormal = true } };

            var repositoryMock = new Mock<IItemReposity>();
            repositoryMock.Setup(x => x.GetItems()).Returns(() => fakeItems);

            var service = new ItemService(repositoryMock.Object);

            // Act
            service.UpdateQuality();
            var items = service.GetItems();

            // Assert
            Assert.AreEqual(0, items.First().Quality);
        }

        [Test]
        public void AgedBrieItemQualityShouldIncrease()
        {
            // Arrange
            var iterations = 3;
            IList<Item> fakeItems = new List<Item> { new Item { Code = "AGEDBRIE", Name = "Aged Brie", SellIn = 10, Quality = 5, IncreasesInQuality = true } };
            IList<Item> savedItems = null;

            var repositoryMock = new Mock<IItemReposity>();
            repositoryMock.Setup(x => x.GetItems()).Returns(() => fakeItems);
            repositoryMock.Setup(x => x.Save(It.IsAny<IList<Item>>())).Callback<IList<Item>>(x => savedItems = x);

            var service = new ItemService(repositoryMock.Object);

            // Act
            for(var i = 0; i < iterations; i++)
            {
                service.UpdateQuality();
            }

            var items = service.GetItems();

            // Assert
            Assert.IsNotNull(savedItems);
            Assert.AreEqual(8, savedItems.First().Quality);
        }

        [Test]
        public void AgedBrieItemQualityShouldNotExceed50()
        {
            // Arrange
            var iterations = 5;
            IList<Item> fakeItems = new List<Item> { new Item { Code = "AGEDBRIE", Name = "Aged Brie", SellIn = 10, Quality = 49, IncreasesInQuality = true } };
            IList<Item> savedItems = null;

            var repositoryMock = new Mock<IItemReposity>();
            repositoryMock.Setup(x => x.GetItems()).Returns(() => fakeItems);
            repositoryMock.Setup(x => x.Save(It.IsAny<IList<Item>>())).Callback<IList<Item>>(x => savedItems = x);

            var service = new ItemService(repositoryMock.Object);

            // Act
            for (var i = 0; i < iterations; i++)
            {
                service.UpdateQuality();
            }

            var items = service.GetItems();

            // Assert
            Assert.IsNotNull(savedItems);
            Assert.AreEqual(50, savedItems.First().Quality);
        }

        [Test]
        public void LegedaryItemsShouldNeverDecreaseInQuantity()
        {
            // Arrange
            var iterations = 5;
            IList<Item> fakeItems = new List<Item> { new Item(80) { Code = "SULFRAG", Name = "Sulfuras, Hand of Ragnaros", SellIn = 25, Quality = 80, HasConstantQuality = true } };
            IList<Item> savedItems = null;

            var repositoryMock = new Mock<IItemReposity>();
            repositoryMock.Setup(x => x.GetItems()).Returns(() => fakeItems);
            repositoryMock.Setup(x => x.Save(It.IsAny<IList<Item>>())).Callback<IList<Item>>(x => savedItems = x);

            var service = new ItemService(repositoryMock.Object);

            // Act
            for (var i = 0; i < iterations; i++)
            {
                service.UpdateQuality();
            }

            var items = service.GetItems();

            // Assert
            Assert.IsNotNull(savedItems);
            Assert.AreEqual(80, savedItems.First().Quality);
        }

        [Test]
        public void BackstagePassItemWhenLessThan10AndMoreThan5DaysShouldIncreaseBy2()
        {
            // Arrange
            var iterations = 2;
            IList<Item> fakeItems = new List<Item> 
            { 
                new Item
                {
                    Code = "BACKSTAGEPASS",
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 8,
                    Quality = 20
                }
            };
            IList<Item> savedItems = null;

            var repositoryMock = new Mock<IItemReposity>();
            repositoryMock.Setup(x => x.GetItems()).Returns(() => fakeItems);
            repositoryMock.Setup(x => x.Save(It.IsAny<IList<Item>>())).Callback<IList<Item>>(x => savedItems = x);

            var service = new ItemService(repositoryMock.Object);

            // Act
            for (var i = 0; i < iterations; i++)
            {
                service.UpdateQuality();
            }

            var items = service.GetItems();

            // Assert
            Assert.IsNotNull(savedItems);
            Assert.AreEqual(24, savedItems.First().Quality);
        }

        [Test]
        public void BackstagePassItemWhenLessThan5DaysShouldIncreaseBy3()
        {
            // Arrange
            var iterations = 2;
            IList<Item> fakeItems = new List<Item>
            {
                new Item
                {
                    Code = "BACKSTAGEPASS",
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 25
                }
            };
            IList<Item> savedItems = null;

            var repositoryMock = new Mock<IItemReposity>();
            repositoryMock.Setup(x => x.GetItems()).Returns(() => fakeItems);
            repositoryMock.Setup(x => x.Save(It.IsAny<IList<Item>>())).Callback<IList<Item>>(x => savedItems = x);

            var service = new ItemService(repositoryMock.Object);

            // Act
            for (var i = 0; i < iterations; i++)
            {
                service.UpdateQuality();
            }

            var items = service.GetItems();

            // Assert
            Assert.IsNotNull(savedItems);
            Assert.AreEqual(31, savedItems.First().Quality);
        }

        [Test]
        public void BackstagePassItemWhenConcertendsShouldQualityBe0()
        {
            // Arrange
            IList<Item> fakeItems = new List<Item>
            {
                new Item
                {
                    Code = "BACKSTAGEPASS",
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 0,
                    Quality = 25
                }
            };
            IList<Item> savedItems = null;

            var repositoryMock = new Mock<IItemReposity>();
            repositoryMock.Setup(x => x.GetItems()).Returns(() => fakeItems);
            repositoryMock.Setup(x => x.Save(It.IsAny<IList<Item>>())).Callback<IList<Item>>(x => savedItems = x);

            var service = new ItemService(repositoryMock.Object);

            // Act
            service.UpdateQuality();

            var items = service.GetItems();

            // Assert
            Assert.IsNotNull(savedItems);
            Assert.AreEqual(0, savedItems.First().Quality);
        }
    }
}
