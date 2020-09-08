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
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private PizzaManager _manager = new PizzaManager();

        [HttpGet]
        public IList<Pizza> Get()
        {
            return _manager.GetAll();
        }
    }
}
