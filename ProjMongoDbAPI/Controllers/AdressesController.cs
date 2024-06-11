using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjMongoDbAPI.Models;
using ProjMongoDbAPI.Service;

namespace ProjMongoDbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressesController : ControllerBase
    {
        private readonly AddressServices addressServices;
        
        public AdressesController
            (AddressServices addressServices)
        {
            this.addressServices = addressServices;
        }
         
        [HttpGet]
        public ActionResult<List<Address>> Get()
        {
            return addressServices.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetAddress")]
        public ActionResult<Address> Get(string id) => addressServices.Get(id);

        [HttpGet("{cep:length(8)}")]
        public ActionResult<AddressDTO> GetPostOffice(string cep)
        {
            return PostOfficeService.GetAddress(cep).Result;
        }
        [HttpPost]  
        public ActionResult<Address> Create(Address address)
        {
            addressServices.Create(address);
            return CreatedAtRoute("GetAddess", new {id = address.id}, address);
        }
    }
}
