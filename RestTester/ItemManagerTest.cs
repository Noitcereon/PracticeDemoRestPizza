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
    public class ItemManagerTest
    {
        private ItemManager _manager;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _manager = new ItemManager();
        }

        //[TestMethod]
        //public void GetWithFilterTest()
        //{
        //    // get x values (between two thresholds)
        //    FilterItem filter = new FilterItem(5, 50);
        //    List<Item> expected = Items.FindAll(x => x.Price <= 50 && x.Price >= 5);
        //    List<Item> actual = new List<Item>(_manager.GetWithFilter(filter));
        //    CollectionAssert.AreEquivalent(expected, actual);

        //    // get x values (above threshold)
        //    filter.HighCost = 0;
        //    expected = Items.FindAll(x => x.Price > filter.LowCost);
        //    actual = new List<Item>(_manager.GetWithFilter(filter));
        //    CollectionAssert.AreEquivalent(expected, actual);

        //    // get nothing, since both filters are 0.
        //    filter.LowCost = 0;


        //    // get x values (below threshold)
        //    filter.HighCost = 40;

        //}

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(7, _manager.GetAll().Count);
        }
        [TestMethod]
        public void GetOneTest()
        {
            List<Item> Items = new List<Item>(_manager.GetAll());
            Assert.AreEqual(Items.Find(x => x.Id == 2).Id,
                            _manager.GetOne(2).Id);
        }
        [TestMethod]
        public void PostTest()
        {
            int itemCountBeforePost = _manager.GetAll().Count;
            _manager.Post(new Item(8, "someName", "Low", 4.99));
            Assert.AreEqual(itemCountBeforePost + 1, _manager.GetAll().Count);
        }
        [TestMethod]
        public void PutTest()
        {
            Item updatedItem = new Item(2, "CrazyItem", "Medium", 50);
            _manager.Put(updatedItem, 2);

            Assert.AreEqual(updatedItem.Name, _manager.GetOne(2).Name);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int itemCountBeforeDelete = _manager.GetAll().Count;
            _manager.Delete(8);
            Assert.AreEqual(itemCountBeforeDelete - 1, _manager.GetAll().Count);
        }
    }
}
