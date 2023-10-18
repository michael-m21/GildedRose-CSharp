using System;
using System.Collections.Generic;
using GildedRose.Entities;
using GildedRose.Repositories;
using GildedRose.Services;

namespace GildedRose
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OMGHAI!");

            // TODO: work on DI
            var service = new ItemService(new ItemRepository());

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                var items = service.GetItems();
                foreach (var item in items)
                {
                    System.Console.WriteLine(item);
                }
                Console.WriteLine("");
                service.UpdateQuality();
            }
        }
    }
}
