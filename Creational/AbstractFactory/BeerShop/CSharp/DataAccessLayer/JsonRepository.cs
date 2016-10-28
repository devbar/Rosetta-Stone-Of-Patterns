using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace BeerShop.DataAccessLayer
{
    public class JsonRepository : IRepository
    {
        private readonly string _filename;

        public JsonRepository(string name)
        {
            _filename = name + ".json";
        }

        public void Insert(IBeer beer)
        {
            var beers = new List<IBeer>(GetAll());
            beers.Add(beer);
            Save(beers);
        }

        public IEnumerable<IBeer> GetAll()
        {
            if (!File.Exists(_filename)) return new List<IBeer>();

            var streamReader = new StreamReader(File.Open(_filename, FileMode.Open));
            var listOfBeers = JsonConvert.DeserializeObject<List<Beer>>(streamReader.ReadToEnd());

            streamReader.Dispose();

            return listOfBeers.Select(b => (IBeer)b);
        }

        private void Save(IList<IBeer> beers)
        {
            if (File.Exists(_filename)) File.Delete(_filename);

            var streamWriter = new StreamWriter(File.Create(_filename));
            var jsonOfBeers = JsonConvert.SerializeObject(beers);

            streamWriter.Write(jsonOfBeers);
            streamWriter.Flush();
            streamWriter.Dispose();
        }

        private class Beer : BeerShop.IBeer
        {
            public Beer(string name, decimal price)
            {
                Name = name;
                Price = price;
            }

            public string Name { get; private set; }

            public decimal Price { get; private set; }
        }
    }
}