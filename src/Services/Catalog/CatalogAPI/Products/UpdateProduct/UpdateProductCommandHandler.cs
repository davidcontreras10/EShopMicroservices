
using CatalogAPI.Exceptions;

namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, decimal Price, string ImageFile) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
	: ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
	public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation($"Updating product with id: {request.Id}");
		var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
		if (product is null)
		{
			throw new ProductNotFoundException(request.Id);
		}

		product.Name = request.Name;
		product.Description = request.Description;
		product.Price = request.Price;
		product.ImageFile = request.ImageFile;

		session.Update(product);
		await session.SaveChangesAsync(cancellationToken);
		return new UpdateProductResult(true);

	}
}