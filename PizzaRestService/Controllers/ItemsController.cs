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
     [Route("api/localItems/")]
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
        public Item GetOne(int id)
        {
            return _manager.GetOne(id);
        }
        [HttpGet]
        [Route("Name/{substring}")]
        public IEnumerable<Item> GetByNameSubstring(string substring)
        {
            return _manager.GetByNameSubstring(substring);
        }
        [HttpGet]
        [Route("Quality/{substring}")]
        public IEnumerable<Item> GetByQualitySubstring(string substring)
        {
            return _manager.GetByQualitySubstring(substring);
        }

        [HttpGet]
        [Route("[search]")]
        public IEnumerable<Item> GetWithFilter([FromQuery] FilterItem filter)
        {
            return _manager.GetWithFilter(filter);
        }

        [HttpPost]
        public String Post([FromBody] Item newItem)
        {
            return _manager.Post(newItem);
        }
        [HttpPut]
        [Route("{id}")]
        public String Put(Item updatedItem, int id)
        {
            return _manager.Put(updatedItem, id);
        }

        [HttpDelete]
        [Route("{id}")]
        public String Delete(int id)
        {
            return _manager.Delete(id);
        }
    }
}
