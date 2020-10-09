using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeRestLib;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace PracticeRestService.Managers
{
    public class ItemManager
    {
        private const string ConnectionString =
            @"Data Source=zealandserver.database.windows.net;Initial Catalog = replaceableDB;
            User ID = zealandserver; Password=iEIPpEfRh#;Connect Timeout = 30;
            Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;
            MultiSubnetFailover=False";

        private List<Item> _items = new List<Item>();

        public IList<Item> GetAll()
        {
            const string sqlQuery = "SELECT * FROM Items";
            using (SqlConnection dbLink = new SqlConnection(ConnectionString))
            {
                using SqlCommand cmd = new SqlCommand(sqlQuery, dbLink);
                dbLink.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _items.Add(ReadNextItem(reader));
                }
                reader.Close();
                dbLink.Close();
            }

            return _items;
        }

        public Item GetOne(int id)
        {
            Item itemWithSpecifiedId = new Item();
            const string sqlQuery = "SELECT * FROM Items WHERE id = @ItemId";
            using (SqlConnection dbLink = new SqlConnection(ConnectionString))
            {
                using SqlCommand cmd = new SqlCommand(sqlQuery, dbLink);
                cmd.Parameters.AddWithValue("@ItemId", id);

                dbLink.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    itemWithSpecifiedId = ReadNextItem(reader);
                }
                reader.Close();
                dbLink.Close();
            }

            return itemWithSpecifiedId;
        }

        public IEnumerable<Item> GetByNameSubstring(string substring)
        {
            _items = new List<Item>(GetAll());

            return _items.FindAll(x => x.Name.ToLower().Contains(substring.ToLower()));
        }

        public IEnumerable<Item> GetByQualitySubstring(string substring)
        {
            _items = new List<Item>(GetAll());

            return _items.FindAll(x => x.Quality.ToLower().Contains(substring.ToLower()));
        }

        public IEnumerable<Item> GetWithFilter(FilterItem filter)
        {
            List<Item> filteredList = new List<Item>();
            _items = new List<Item>(GetAll());

            // TODO: Refactor
            if (filter.HighCost > 0 && filter.LowCost > 0)
            {
                filteredList = _items.FindAll((x) => x.Price <= filter.HighCost && x.Price >= filter.LowCost);
            }
            else if (filter.HighCost > 0 && filter.LowCost == 0)
            {
                filteredList = _items.FindAll(x => x.Price <= filter.HighCost);
            }
            else if (filter.HighCost == 0 && filter.LowCost > 0)
            {
                filteredList = _items.FindAll(x => x.Price >= filter.LowCost);
            }

            return filteredList;
        }

        public String Post(Item newItem)
        {
            try
            {
                string sqlQuery = "INSERT INTO Item (Name, Quality, Price)" +
                                  "VALUES (@itemName, @itemQuality, @itemPrice)";
                using SqlConnection dbLink = new SqlConnection(ConnectionString);
                using SqlCommand cmd = new SqlCommand(sqlQuery, dbLink);
                cmd.Parameters.AddWithValue(@"itemName", newItem.Name);
                cmd.Parameters.AddWithValue(@"itemQuality", newItem.Quality);
                cmd.Parameters.AddWithValue(@"itemPrice", newItem.Price);
                dbLink.Open();
                cmd.ExecuteNonQuery();
                dbLink.Close();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

            //_items.Add(newItem);
            return $"{ newItem } added.";
        }

        public String Put(Item updatedItem, int id)
        {
            var oldItem = _items.First(x => x.Id == id);
            oldItem.Name = updatedItem.Name;
            oldItem.Quality = updatedItem.Quality;
            oldItem.Price = updatedItem.Price;

            return $"Updated item Nr. {id}";
        }

        public String Delete(int id)
        {
            Item itemToDelete = GetOne(id);
            _items.Remove(itemToDelete);

            return $"Deleted item: {itemToDelete}";
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
    }
}
