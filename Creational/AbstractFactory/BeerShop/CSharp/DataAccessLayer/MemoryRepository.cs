using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BeerShop.DataAccessLayer
{
    public class MemoryRepository : IRepository
    {
        private static readonly ConcurrentDictionary<string, List<IBeer>> List = new ConcurrentDictionary<string, List<IBeer>>();

        private readonly string _name;

        public MemoryRepository(string name)
        {
            _name = name;
        }

        public void Insert(IBeer beer)
        {
            List<IBeer> beers;
            if (!List.TryGetValue(_name, out beers))
            {
                beers = new List<IBeer>();
                List.TryAdd(_name, beers);
            }

            beers.Add(beer);
        }
        public IEnumerable<IBeer> GetAll()
        {
            List<IBeer> beers;
            if (!List.TryGetValue(_name, out beers))
                return new List<IBeer>();

            return beers;
        }
    }
}