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
            new Item(1, "Cheese", "High",(decimal)(55.5)),
            new Item(2, "Sausage", "Medium",(decimal)(35.5)),
            new Item(3, "Bun", "Low", (decimal)9.95),
            new Item(4, "Pork", "Medium", (decimal)30.95),
            new Item(5, "Beef", "High", (decimal)69.95),
            new Item(6, "Milk", "High", (decimal)9.95),
            new Item(7, "Cheesecake", "Medium,", (decimal)19.95)
        };

        public IList<Item> GetAll()
        {
            return Items;
        }
        public Item GetOne(int id)
        {
            return Items.Find((item) => item.Id == id);
        }

        public IEnumerable<Item> GetFromSubstring(string substring)
        {
            return Items.FindAll(x => x.Name.ToLower().Contains(substring.ToLower()));
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
