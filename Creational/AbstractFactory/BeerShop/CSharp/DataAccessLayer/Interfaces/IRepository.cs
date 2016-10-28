using System.Collections.Generic;

namespace BeerShop.DataAccessLayer
{
    public interface IRepository
    {
        void Insert(IBeer beer);
        IEnumerable<IBeer> GetAll();
    }
}