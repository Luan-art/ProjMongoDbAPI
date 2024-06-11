using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjMongoDbAPI.Models;
using ProjMongoDbAPI.Service;

namespace ProjMongoDbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersService custumersService;
        private readonly AddressServices addressService;

        public CustomersController
            (CustomersService custumersService, AddressServices addressService)
        {
            this.custumersService = custumersService;
            this.addressService = addressService;
        }

        [HttpGet]
        public ActionResult<List<Customers>> Get()
        {
            return custumersService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetCustumersById")]
        public ActionResult<Customers> Get(string id)
        {
            var custumer = custumersService.Get(id);
                
            if(custumer == null)
            {
                return NotFound();
            }
            return Ok(custumer);
        
        }

        [HttpPost]
        public ActionResult<Customers> Create(Customers customers)
        {
            Address address = addressService.Create(customers.Address);
            
            custumersService.Create(customers);
            return CreatedAtRoute("GetCustumersById", new { id = customers.Id }, customers);
        }
    }
}
