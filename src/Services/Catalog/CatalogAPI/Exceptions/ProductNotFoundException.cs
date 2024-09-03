namespace CatalogAPI.Exceptions
{
	public class ProductNotFoundException : Exception
	{
		public ProductNotFoundException(Guid id)
			: base($"Product with id {id} not found")
		{
		}
	}
}
