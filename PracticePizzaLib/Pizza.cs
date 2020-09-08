using System;
using System.Collections.Generic;
using System.Text;

namespace PracticePizzaLib
{
    public class Pizza
    {
        private int _number;
        private String _description;
        private bool _familySize;
        private decimal _price;

        public Pizza(int number, String description, bool familySize, decimal price)
        {
            _number = number;
            _description = description;
            _familySize = familySize;
            _price = price;
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public String Description
        {
            get { return _description;}
            set { _description = value; }
        }

        public bool FamilySize
        {
            get { return _familySize; }
            set { _familySize = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public override string ToString()
        {
            return $"Nr. {Number}, {Description}, {Price}dkk";
        }
    }
}
