using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaRestService.Managers;
using PracticePizzaLib;

namespace PizzaRestService.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaManager _manager = new PizzaManager();

        [HttpGet]
        public IList<Pizza> Get()
        {
            return _manager.GetAll();
        }

        [HttpGet]
        [Route("{number}")]
        public Pizza GetOne(int number)
        {
            return _manager.GetOne(number);
        }

        [HttpPost]
        public String Post([FromBody]Pizza newPizza)
        {
            return _manager.Post(newPizza);
        }
        [HttpPut]
        [Route("{number}")]
        public String Put(Pizza updatedPizza, int number)
        {
            return _manager.Put(updatedPizza, number);
        }

        [HttpDelete]
        [Route("{number}")]
        public String Delete(int number)
        {
            return _manager.Delete(number);
        }
    }
}
