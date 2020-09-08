using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Threading.Tasks;
using PracticePizzaLib;

namespace PizzaRestService.Managers
{
    public class PizzaManager
    {
        private static readonly List<Pizza> Pizzas = new List<Pizza>
        {
            new Pizza(1, "First pizza", false, Convert.ToDecimal(50.95)),
            new Pizza(2, "Second pizza", true, Convert.ToDecimal(69.95)),
            new Pizza(3, "Third pizza", true, Convert.ToDecimal(90.95)),
            new Pizza(4, "Fourth pizza", true, Convert.ToDecimal(79.95)),
            new Pizza(5, "Fifth pizza", false, Convert.ToDecimal(49.95)),
            new Pizza(6, "Sixth pizza", false, Convert.ToDecimal(30.0)),
        };

        public IList<Pizza> GetAll()
        {
            return Pizzas;
        }

        public Pizza GetOne(int number)
        {
            return Pizzas.Find((pizza) => pizza.Number == number);
        }

        public String Post(Pizza newPizza)
        {
            Pizzas.Add(newPizza);

            return $"{ newPizza } added.";
        }

        public String Put(Pizza newPizza, int number)
        {
            Pizza oldPizza = GetOne(number);


            return $"Updated {oldPizza} to be {newPizza}";
        }

        public String Delete(int number)
        {
            Pizza pizzaToDelete = GetOne(number);

            return $"Deleted pizza: {pizzaToDelete}";
        }
    }
}
