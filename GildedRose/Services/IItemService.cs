using GildedRose.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Services
{
    public interface IItemService
    {
        IReadOnlyCollection<Item> GetItems();
        void UpdateQuality();
    }
}
