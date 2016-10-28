using Autofac;

namespace BeerShop.DataAccessLayer
{
    public class MemoryRepositoryFactory : IRepositoryFactory
    {
        private readonly ILifetimeScope _lifeTimeScope;

        public MemoryRepositoryFactory(ILifetimeScope lifeTimeScope)
        {
            _lifeTimeScope = lifeTimeScope;
        }

        public IRepository Create(string name)
        {
            return _lifeTimeScope
                .Resolve<MemoryRepository>(new TypedParameter(typeof(string), name));
        }
    }
}