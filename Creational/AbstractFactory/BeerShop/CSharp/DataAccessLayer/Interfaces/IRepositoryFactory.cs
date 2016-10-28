namespace BeerShop.DataAccessLayer
{
    public interface IRepositoryFactory
    {
        IRepository Create(string name);
    }

    public enum RepositoryType
    {
        Json,
        Memory
    }
}