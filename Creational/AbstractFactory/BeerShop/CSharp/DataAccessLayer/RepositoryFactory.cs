using Autofac;

namespace BeerShop.DataAccessLayer
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly ILifetimeScope _lifeTimeScope;

        public RepositoryFactory(ILifetimeScope lifeTimeScope){
            _lifeTimeScope = lifeTimeScope;
        }

        public IRepository Create(RepositoryType type)        
        {
            if(type == RepositoryType.Json)
                return _lifeTimeScope.Resolve<IJsonRepository>();
            else
                return _lifeTimeScope.Resolve<IMemoryRepository>();
        }
    }    
}