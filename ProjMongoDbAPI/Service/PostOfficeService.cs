using Newtonsoft.Json;
using ProjMongoDbAPI.Models;

namespace ProjMongoDbAPI.Service
{
    public class PostOfficeService
    {
        static readonly HttpClient address = new HttpClient();

        public static async Task<AddressDTO> GetAddress(string cep)
        {
            HttpResponseMessage responde = await address.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            responde.EnsureSuccessStatusCode();
            string add = await responde.Content.ReadAsStringAsync();
            AddressDTO addressDTO = JsonConvert.DeserializeObject<AddressDTO>(add);

            return addressDTO;

        }
    }
}
