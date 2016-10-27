using System.Collections.Generic;
using System.Linq;
using BeerShop.DataAccessLayer;

namespace BeerShop{
	public class BeerShopService : IBeerShopService {
		
		private readonly IRepository _repository;
		
		private readonly IBeerFactory _beerFactory;		
		
		public BeerShopService(IBeerFactory beerFactory, IRepositoryFactory repositoryFactory, RepositoryType type){
			_beerFactory = beerFactory;	
			_repository = repositoryFactory.Create(type);			
		}

		public void Add(string name, decimal price, uint amount){
			for(var i = 0; i < amount; i++)
				_repository.Insert(_beerFactory.Create(name, price));
		}

		public IList<IBeer> Search(string term){
			var beers = _repository.GetAll();

			return beers.Where(b => b.Name.Contains(term)).ToList();
		}
	}
}