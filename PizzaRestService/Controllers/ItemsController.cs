using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeRestLib;
using PracticeRestService.Managers;

namespace PracticeRestService.Controllers
{
     [ApiController]
     [Route("api/Items/")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemManager _manager = new ItemManager();

        [HttpGet]
        public IList<Item> Get()
        {
            return _manager.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Item GetOne(int number)
        {
            return _manager.GetOne(number);
        }
        [HttpGet]
        [Route("Name/{substring}")]
        public IEnumerable<Item> GetFromSubstring(string substring)
        {
            return _manager.GetFromSubstring(substring);
        }

        [HttpPost]
        public String Post([FromBody] Item newItem)
        {
            return _manager.Post(newItem);
        }
        [HttpPut]
        [Route("{number}")]
        public String Put(Item updatedItem, int number)
        {
            return _manager.Put(updatedItem, number);
        }

        [HttpDelete]
        [Route("{number}")]
        public String Delete(int number)
        {
            return _manager.Delete(number);
        }
    }
}
