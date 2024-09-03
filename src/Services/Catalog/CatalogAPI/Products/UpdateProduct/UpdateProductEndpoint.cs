
namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, List<string> Categories, string Description, decimal Price, string ImageFile);

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/products", async (ISender sender, UpdateProductRequest request) =>
		{
			var command = request.Adapt<UpdateProductCommand>();
			var result = await sender.Send(command);
			var response = result.Adapt<UpdateProductResponse>();
			return Results.Ok(response);
		})
		.WithName("UpdateProduct")
		.Produces<UpdateProductResponse>(StatusCodes.Status200OK, "application/json")
		.ProducesProblem(StatusCodes.Status400BadRequest, "application/json")
		.ProducesProblem(StatusCodes.Status404NotFound, "application/json")
		.WithSummary("Update a product")
		.WithDescription("Update a product in the catalog");
	}
}