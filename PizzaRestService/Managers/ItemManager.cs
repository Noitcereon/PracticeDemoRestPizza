using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeRestLib;

namespace PracticeRestService.Managers
{
    public class ItemManager
    {
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

        public IList<Item> GetAll()
        {
            return Items;
        }
        public Item GetOne(int id)
        {
            return Items.Find((item) => item.Id == id);
        }

        public IEnumerable<Item> GetByNameSubstring(string substring)
        {
            return Items.FindAll(x => x.Name.ToLower().Contains(substring.ToLower()));
        }

        public IEnumerable<Item> GetByQualitySubstring(string substring)
        {
            return Items.FindAll(x => x.Quality.ToLower().Contains(substring.ToLower()));
        }

        public IEnumerable<Item> GetWithFilter(FilterItem filter)
        {
            throw new NotImplementedException();
            // code that filters according to the values in filter.
        }

        public String Post(Item newItem)
        {
            Items.Add(newItem);
            return $"{ newItem } added.";
        }

        public String Put(Item updatedItem, int id)
        {
            var oldItem = Items.First(x => x.Id == id);
            oldItem.Name = updatedItem.Name;
            oldItem.Quality = updatedItem.Quality;
            oldItem.Price = updatedItem.Price;

            return $"Updated item Nr. {id}";
        }

        public String Delete(int id)
        {
            Item itemToDelete = GetOne(id);
            Items.Remove(itemToDelete);

            return $"Deleted item: {itemToDelete}";
        }
    }
}
