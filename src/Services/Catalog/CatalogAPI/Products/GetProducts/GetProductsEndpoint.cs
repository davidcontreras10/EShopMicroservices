namespace CatalogAPI.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : CarterModule
{
	public override void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products", async (ISender sender) =>
		{
			var query = new GetProductsQuery();
			var result = await sender.Send(query);
			var response = result.Adapt<GetProductsResponse>();
			return Results.Ok(response);
		})
		.WithName("GetProducts")
		.Produces<GetProductsResponse>(StatusCodes.Status200OK, "application/json")
		.ProducesProblem(StatusCodes.Status400BadRequest, "application/json")
		.WithSummary("Get all products")
		.WithDescription("Get all products in the catalog");
	}
}
