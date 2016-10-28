using Autofac;

namespace BeerShop.DataAccessLayer
{
    public class JsonRepositoryFactory : IRepositoryFactory
    {
        private readonly ILifetimeScope _lifeTimeScope;

        public JsonRepositoryFactory(ILifetimeScope lifeTimeScope)
        {
            _lifeTimeScope = lifeTimeScope;
        }

        public IRepository Create(string name)
        {
            return _lifeTimeScope
                .Resolve<JsonRepository>(new TypedParameter(typeof(string), name));
        }
    }
}