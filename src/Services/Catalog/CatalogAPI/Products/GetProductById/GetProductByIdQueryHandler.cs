
using CatalogAPI.Exceptions;

namespace CatalogAPI.Products.GetProductById
{
	public record class GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

	public record class GetProductByIdResult(Product Product);

	public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
		: IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
	{
		public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Getting product with id: {request.Id}");
			var product = await session.LoadAsync<Product>(request.Id);
			if (product is null)
			{
				throw new ProductNotFoundException(request.Id);
			}


			return new GetProductByIdResult(product);
		}
	}
}
