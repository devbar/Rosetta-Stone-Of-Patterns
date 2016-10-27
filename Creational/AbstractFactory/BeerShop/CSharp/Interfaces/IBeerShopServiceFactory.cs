using BeerShop.DataAccessLayer;

namespace BeerShop{
    public interface IBeerShopServiceFactory{
        IBeerShopService Create(RepositoryType type);
    }
}