using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace BeerShop.DataAccessLayer{
    public class JsonRepository : IJsonRepository {
        private const string FileName = "beers.json";        

        public void Insert(IBeer beer){
            var beers = new List<IBeer>(GetAll());
            beers.Add(beer);
            Save(beers);
        }       

        public IEnumerable<IBeer> GetAll(){
            if(!File.Exists(FileName)) return new List<IBeer>();

            var streamReader = new StreamReader(File.Open(FileName, FileMode.Open));
            var listOfBeers = JsonConvert.DeserializeObject<List<Beer>>(streamReader.ReadToEnd());
            
            streamReader.Dispose();

            return listOfBeers.Select(b => (IBeer)b);
        }

        private void Save(IList<IBeer> beers){
            if(File.Exists(FileName)) File.Delete(FileName);

            var streamWriter = new StreamWriter(File.Create(FileName));
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