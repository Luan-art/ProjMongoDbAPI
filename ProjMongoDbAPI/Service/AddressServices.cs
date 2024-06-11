using MongoDB.Driver;
using ProjMongoDbAPI.Models;
using ProjMongoDbAPI.Utils;

namespace ProjMongoDbAPI.Service
{
    public class AddressServices
    {
        private readonly IMongoCollection<Address> _address;

        public AddressServices(IProjMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);

        }

        public List<Address> Get()
        {
            return _address.Find(address => true).ToList();
        }

        public Address Get(string id) => _address.Find<Address>(address => address.id == id).FirstOrDefault();

        public Address Create (Address address)
        {
            _address.InsertOne(address);
            return address;
        }
      
    }
}
