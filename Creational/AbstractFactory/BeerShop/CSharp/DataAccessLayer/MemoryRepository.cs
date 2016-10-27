using System.Collections.Generic;

namespace BeerShop.DataAccessLayer{        
    public class MemoryRepository : IMemoryRepository{
        private readonly IList<IBeer> _list = new List<IBeer>();
        public void Insert(IBeer beer){
            _list.Add(beer);
        }       
        public IEnumerable<IBeer> GetAll(){
            return _list;
        }
    }
}