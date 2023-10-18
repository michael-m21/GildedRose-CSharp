using GildedRose.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Repositories
{
    public interface IItemReposity
    {
        IList<Item> GetItems();
    }
}
