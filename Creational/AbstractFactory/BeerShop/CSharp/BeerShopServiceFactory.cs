using Autofac;
using BeerShop.DataAccessLayer;

namespace BeerShop
{
    public class BeerShopServiceFactory : IBeerShopServiceFactory
    {
        private readonly ILifetimeScope _lifeTimeScope;
        public BeerShopServiceFactory(ILifetimeScope lifeTimeScope)
        {
            _lifeTimeScope = lifeTimeScope;
        }

        public IBeerShopService Create(RepositoryType type)
        {
            return _lifeTimeScope.Resolve<IBeerShopService>(new TypedParameter(typeof(RepositoryType), type));
        }
    }
}