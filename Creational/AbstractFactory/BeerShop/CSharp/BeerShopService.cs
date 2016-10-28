using System.Collections.Generic;
using System.Linq;
using Autofac;
using BeerShop.DataAccessLayer;

namespace BeerShop
{
    public class BeerShopService : IBeerShopService
    {

        private readonly IRepository _repository;

        private readonly IBeerFactory _beerFactory;

        public BeerShopService(ILifetimeScope lifeTimeScope, IBeerFactory beerFactory, RepositoryType type)
        {
            _beerFactory = beerFactory;
            _repository = lifeTimeScope
                .ResolveKeyed<IRepositoryFactory>(type)
                .Create("beer");
        }

        public void Add(string name, decimal price, uint amount)
        {
            for (var i = 0; i < amount; i++)
                _repository.Insert(_beerFactory.Create(name, price));
        }

        public IList<IBeer> Search(string term)
        {
            var beers = _repository.GetAll();

            return beers.Where(b => b.Name.Contains(term)).ToList();
        }
    }
}