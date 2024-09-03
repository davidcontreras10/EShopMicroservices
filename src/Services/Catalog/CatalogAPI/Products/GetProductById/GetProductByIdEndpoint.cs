
namespace CatalogAPI.Products.GetProductById;

public record class GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/{id}", async (ISender sender, Guid id) =>
		{
			var query = new GetProductByIdQuery(id);
			var result = await sender.Send(query);
			var response = result.Adapt<GetProductByIdResponse>();
			return Results.Ok(response);
		})
		.WithName("GetProductById")
		.Produces<GetProductByIdResponse>(StatusCodes.Status200OK, "application/json")
		.ProducesProblem(StatusCodes.Status400BadRequest, "application/json")
		.ProducesProblem(StatusCodes.Status404NotFound, "application/json")
		.WithSummary("Get a product by id")
		.WithDescription("Get a product in the catalog by its id");
	}
}