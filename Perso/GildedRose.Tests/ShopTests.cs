using System.IO;
using GildedRose;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose.Tests
{
    [TestClass]
    public class ShopTests
    {
        private Shop shop;

[TestInitialize]
public void Setup()
{
    var items = new Item[] {
        new GenericItem("Generic Item 1", 5, 3),
        new GenericItem("Generic Item 2", -2, 6),
        new GenericItem("Generic Item 3", 6, 0),
        new AgedItem("Aged Brie", 5, 6),
        new AgedItem("Aged Brie", 5, 50),
        new LegendaryItem("Sulfuras", 0, 10),
        new EventItem("Backstage passes", 12, 10),
        new EventItem("Backstage passes", 8, 10),
        new EventItem("Backstage passes", 4, 10),
        new EventItem("Backstage passes", 0, 10),
        new ConjuredItem("Conjured Item 1", 5, 6),
        new ConjuredItem("Conjured Item 1", -3, 12),
    };

    var repo = new InMemoryItemRepository();
    repo.SaveInventory(items);
    this.shop = new Shop(repo);
}

    [TestClass]
    public class FileItemsRepositoryTests
    {
        private string testFilePath = "test_inventory.json";

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [TestMethod]
        public void Should_SaveAndLoadInventoryFromFile()
        {
            // Arrange
            var items = new Item[]
            {
                new GenericItem("Test Item", 10, 20)
            };

            var repo = new FileItemsRepository(testFilePath);

            // Act
            repo.SaveInventory(items);
            var loadedItems = repo.GetInventory();

            // Assert
            Assert.AreEqual(1, loadedItems.Length);
            Assert.AreEqual("Test Item", loadedItems[0].Name);
            Assert.AreEqual(10, loadedItems[0].SellIn);
            Assert.AreEqual(20, loadedItems[0].Quality);
        }
    }


        [TestMethod]
        public void Should_UpdateInventory()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(4, this.shop.Inventory[0].SellIn);
            Assert.AreEqual(2, this.shop.Inventory[0].Quality);
        }

        [TestMethod]
        public void Should_DecreaseQualityByTwoAfterExpiration()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(4, this.shop.Inventory[1].Quality);
        }

        [TestMethod]
        public void Should_NotDecreaseQualityUnderZero()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(0, this.shop.Inventory[2].Quality);
        }

        [TestMethod]
        public void Should_IncreaseAgedBrieQuality()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(7, this.shop.Inventory[3].Quality);
        }

        [TestMethod]
        public void Should_NotIncreaseQualityOverFifty()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(50, this.shop.Inventory[4].Quality);
        }

        [TestMethod]
        public void Should_NotChangeSulfurasQuality()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(10, this.shop.Inventory[5].Quality);
        }

        [TestMethod]
        public void Should_IncreaseBackstagePassQuality()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(11, this.shop.Inventory[6].Quality);
        }

        [TestMethod]
        public void Should_IncreaseBackstagePassQualityByTwoTenDaysBefore()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(12, this.shop.Inventory[7].Quality);
        }

        [TestMethod]
        public void Should_IncreaseBackstagePassQualityByThreeFiveDaysBefore()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(13, this.shop.Inventory[8].Quality);
        }

        [TestMethod]
        public void Should_SetBackstagePassQualityToZeroAfterEvent()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(0, this.shop.Inventory[9].Quality);
        }

        [TestMethod]
        public void Should_DecreaseConjuredItemQualityTwiceAsFast()
        {
            this.shop.UpdateInventory();
            Assert.AreEqual(4, this.shop.Inventory[10].Quality);
            Assert.AreEqual(8, this.shop.Inventory[11].Quality);
        }
    }
}