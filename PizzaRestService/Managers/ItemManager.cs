using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeRestLib;
using System.Data.SqlClient;

namespace PracticeRestService.Managers
{
    public class ItemManager
    {
        private const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;
            Initial Catalog=ItemDB;Integrated Security=True;Connect Timeout=30;
            Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;
            MultiSubnetFailover=False";

        private readonly List<Item> items = new List<Item>();

        public IList<Item> GetAll()
        {
            const string sqlQuery = "SELECT * FROM Item";
            using (SqlConnection dbLink = new SqlConnection(ConnectionString))
            {
                using SqlCommand cmd = new SqlCommand(sqlQuery, dbLink);
                dbLink.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(ReadNextItem(reader));
                }
                reader.Close();
                dbLink.Close();
            }

            return items;
        }

        private Item ReadNextItem(SqlDataReader reader)
        {
            Item nextItem = new Item
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Quality = reader.GetString(2),
                Price = (double)reader.GetDecimal(3)
            };

            return nextItem;
        }

        public Item GetOne(int id)
        {
            return items.Find((item) => item.Id == id);
        }

        public IEnumerable<Item> GetByNameSubstring(string substring)
        {
            return items.FindAll(x => x.Name.ToLower().Contains(substring.ToLower()));
        }

        public IEnumerable<Item> GetByQualitySubstring(string substring)
        {
            return items.FindAll(x => x.Quality.ToLower().Contains(substring.ToLower()));
        }

        public IEnumerable<Item> GetWithFilter(FilterItem filter)
        {
            List<Item> filteredList = new List<Item>();

            // TODO: Refactor
            if (filter.HighCost > 0 && filter.LowCost > 0)
            {
                filteredList = items.FindAll((x) => x.Price <= filter.HighCost && x.Price >= filter.LowCost);
            }
            else if (filter.HighCost > 0 && filter.LowCost == 0)
            {
                filteredList = items.FindAll(x => x.Price <= filter.HighCost);
            }
            else if (filter.HighCost == 0 && filter.LowCost > 0)
            {
                filteredList = items.FindAll(x => x.Price >= filter.LowCost);
            }

            return filteredList;
        }

        public String Post(Item newItem)
        {
            items.Add(newItem);
            return $"{ newItem } added.";
        }

        public String Put(Item updatedItem, int id)
        {
            var oldItem = items.First(x => x.Id == id);
            oldItem.Name = updatedItem.Name;
            oldItem.Quality = updatedItem.Quality;
            oldItem.Price = updatedItem.Price;

            return $"Updated item Nr. {id}";
        }

        public String Delete(int id)
        {
            Item itemToDelete = GetOne(id);
            items.Remove(itemToDelete);

            return $"Deleted item: {itemToDelete}";
        }
    }
}
