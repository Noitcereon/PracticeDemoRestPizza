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
        private ItemManager _manager;
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

        [TestInitialize]
        public void BeforeEachTest()
        {
            _manager = new ItemManager();
        }
        
        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(7, _manager.GetAll().Count);
        }
        [TestMethod]
        public void GetOneTest()
        {
            Assert.AreEqual(Items.Find(x => x.Id == 2).Id,
                            _manager.GetOne(2).Id);
        }
        [TestMethod]
        public void PostTest()
        {
            int itemCountBeforePost = _manager.GetAll().Count;
            _manager.Post(new Item(8, "someName", "Low", 4.99));
            Assert.AreEqual(itemCountBeforePost +1, _manager.GetAll().Count);
        }
        [TestMethod]
        public void PutTest()
        {
            Item updatedItem = new Item(1, "CrazyItem", "Medium", 50);
            _manager.Put(updatedItem, 1);

            Assert.AreEqual(updatedItem.Name, _manager.GetOne(1).Name);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int itemCountBeforeDelete = _manager.GetAll().Count;
            _manager.Delete(1);
            Assert.AreEqual(itemCountBeforeDelete -1, _manager.GetAll().Count);
        }
        [TestMethod]
        public void GetWithFilterTest()
        {
            // get x values (between two thresholds)
            FilterItem filter = new FilterItem(5, 50);
            List<Item> expected = Items.FindAll(x => x.Price <= 50 && x.Price >= 5);
            List<Item> actual = new List<Item>(_manager.GetWithFilter(filter));
            CollectionAssert.AreEquivalent(expected, actual);

            // get x values (above threshold)
            filter.HighCost = 0;
            expected = Items.FindAll(x => x.Price > filter.LowCost);
            actual = new List<Item>(_manager.GetWithFilter(filter));
            CollectionAssert.AreEquivalent(expected, actual);

            // get nothing, since both filters are 0.
            filter.LowCost = 0;


            // get x values (below threshold)
            filter.HighCost = 40;

        }

    }
}
