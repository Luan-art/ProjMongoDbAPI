using MongoDB.Driver;
using ProjMongoDbAPI.Models;
using ProjMongoDbAPI.Utils;

namespace ProjMongoDbAPI.Service
{
    public class CustomersService
    {
        private readonly IMongoCollection<Customers> _customer;

        public CustomersService(IProjMongoDBAPIDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _customer = database.GetCollection<Customers>(settings.CustomerCollectionName);

                
        }

        public List<Customers> Get() => _customer.Find(customer => true).ToList();

        public Customers Get(string id) => _customer.Find<Customers>(customer => customer.Id == id).FirstOrDefault();

        public Customers Create(Customers customers)
        {
             _customer.InsertOne(customers);
            return customers;
        }

        public Customers Update(Customers customers)
        {
            _customer.ReplaceOne(c => c.Id == customers.Id, customers);
            return customers;
        }

        public void Delete(string id)
        {
            _customer.DeleteOne(c => c.Id == id);

        }

    }
}
