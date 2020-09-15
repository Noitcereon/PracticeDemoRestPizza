using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeRestLib
{
    public class Item
    {
        private int _id;
        private string _name;
        private string _quality;
        private double _price;

        public Item() { }

        public Item(int id, string name, string quality, double price)
        {
            Id = id;
            Name = name;
            Quality = quality;
            Price = price;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Quality
        {
            get => _quality;
            set => _quality = value;
        }

        public double Price
        {
            get => _price;
            set => _price = value;
        }
    }
}
