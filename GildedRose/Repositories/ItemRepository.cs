using GildedRose.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Repositories
{
    public class ItemRepository: IItemReposity
    {
        private static readonly IList<Item> items;

        static ItemRepository()
        {
            items = BuildItems();
        }

        public IList<Item> GetItems()
        {
            // Simulate persistent data get
            return items;
        }

        private static IList<Item> BuildItems() => new List<Item>{
                new Item 
                {
                    Code = "DEXTVEST",
                    Name = "+5 Dexterity Vest",
                    SellIn = 10,
                    Quality = 20,
                    IsNormal = true,
                },
                new Item 
                {
                    Code = "AGEDBRIE",
                    Name = "Aged Brie",
                    SellIn = 2,
                    Quality = 0,
                    IncreasesInQuality = true,
                },
                new Item 
                {
                    Code = "EXILMONGOOSE",
                    Name = "Elixir of the Mongoose",
                    SellIn = 5,
                    Quality = 7,
                    IsNormal = true,
                },
                new Item(80)
                {
                    Code = "SULFRAG",
                    Name = "Sulfuras, Hand of Ragnaros",
                    SellIn = 0,
                    Quality = 80,
                    HasConstantQuality = true,
                },
                new Item(80)
                {
                    Code = "SULFRAG",
                    Name = "Sulfuras, Hand of Ragnaros",
                    SellIn = -1,
                    Quality = 80,
                    HasConstantQuality = true,
                },
                new Item
                {
                    Code = "BACKSTAGEPASS",
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Code = "BACKSTAGEPASS",
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new Item
                {   
                    Code = "BACKSTAGEPASS",
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
				// this conjured item does not work properly yet
				//new Item 
    //            {
    //                Code = "CONJ",
    //                Name = "Conjured Mana Cake",
    //                SellIn = 3,
    //                Quality = 6,
    //                IsNormal = true,
    //            }
            };

        public void Save(IList<Item> items)
        {
            // Simulate save to be captured in mocking
        }
    }
}
