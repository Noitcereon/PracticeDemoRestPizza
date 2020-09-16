using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeRestLib;
using PracticeRestService.Managers;

namespace RestTester
{
    [TestClass]
    public class ItemsControllerTest
    {
        private static readonly ItemManager Manager = new ItemManager();
        private static readonly List<Item> Items = new List<Item>
        {
            new Item(1, "Cheese", "High",55.5),
            new Item(2, "Sausage", "Medium",35.5),
            new Item(3, "Bun", "Low", 9.95),
            new Item(4, "Pork", "Medium", 30.95),
            new Item(5, "Beef", "High", 69.95),
            new Item(6, "Milk", "High", 9.95),
            new Item(7, "Cheesecake", "Medium", 19.95)
        };

        [TestMethod]
        public void GetWithFilterTest()
        {
            // get x values (above threshold)
            // get x values (below threshold)
            // get x values (between two thresholds)

            FilterItem filter = new FilterItem(5, 50);

            List<Item> expected = Items.FindAll(x => x.Price <= filter.HighCost && x.Price >= filter.LowCost);
            List<Item> actual = new List<Item>(Manager.GetWithFilter(filter));
            
            // this throws an error for some reason.
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
