using GildedRose.Entities;
using GildedRose.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GildedRose.Specifications;

namespace GildedRose.Services
{
    public class ItemService: IItemService
    {
        private readonly IItemReposity itemRepository;

        public ItemService(IItemReposity itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IReadOnlyCollection<Item> GetItems()
        {
            return this.itemRepository.GetItems().ToList().AsReadOnly();
        }

        public void UpdateQuality()
        {
            var items = this.itemRepository.GetItems();

            foreach (var item in items)
            {
                this.UpdateItemQuality(item);

                if (new ConstantQualityItemSpecification().IsSatisfiedBy(item))
                {
                    item.SellIn -= 1;
                }
            }

            itemRepository.Save(items);
        }

        private void UpdateItemQuality(Item item)
        {
            try
            {
                if (new ConstantQualityItemSpecification().IsSatisfiedBy(item))
                {
                    return;
                }

                if (new IncreasesInQualityItemSpecification().IsSatisfiedBy(item))
                {
                    item.Quality += 1;
                    return;
                }

                if (new SellInOverdueItemSpecification().IsSatisfiedBy(item))
                {
                    item.Quality -= item.Quality;
                    return;
                }

                if (new NormalItemSpecification().IsSatisfiedBy(item))
                {
                    item.Quality -= 1;
                    return;
                }

                if (new BackstagePassItemSpecification().IsSatisfiedBy(item))
                {
                    this.UpdateBackstagePassQuantity(item);
                    return;
                }
            }
            catch(QualityBelowZeroException)
            {
                item.Quality = 0;
            }
            catch(OverQualityException)
            {
                item.Quality = item.MaxQuality;
            }
        }

        private void UpdateBackstagePassQuantity(Item item)
        {
            if (item.SellIn <= 0)
            {
                item.Quality = 0;
                return;
            }

            if (item.SellIn < 6)
            {
                item.Quality += 3;
                return;
            }

            if (item.SellIn < 11)
            {
                item.Quality += 2;
                return;
            }
        }
    }
}
