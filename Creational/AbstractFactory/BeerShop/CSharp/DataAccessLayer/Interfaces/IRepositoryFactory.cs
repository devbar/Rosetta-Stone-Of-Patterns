namespace BeerShop.DataAccessLayer
{
	public interface IRepositoryFactory
	{
		IRepository Create(RepositoryType type);
	}

	public enum RepositoryType{
        Json,
        Memory
    }
}