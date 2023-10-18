using GildedRose.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Specifications
{
    public class SellInOverdueItemSpecification : Specification<Item>
    {
        protected override Expression<Func<Item, bool>> Expression()
        {
            return item => item.SellIn < 0;
        }
    }
}
