using System.Collections.Generic;

namespace BeerShop
{
    public interface IBeerShopService
    {
        void Add(string name, decimal price, uint amount);
        IList<IBeer> Search(string term);
    }
}