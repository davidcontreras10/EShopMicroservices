namespace CatalogAPI.Products.GetProductsByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : CarterModule
{
	public override void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/category/{category}", async (ISender sender, string category) =>
		{
			var query = new GetProductByCategoryQuery(category);
			var result = await sender.Send(query);
			var response = result.Adapt<GetProductByCategoryResponse>();
			return Results.Ok(response);
		})
		.WithName("GetProductByCategory")
		.Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK, "application/json")
		.ProducesProblem(StatusCodes.Status400BadRequest, "application/json")
		.WithSummary("Get all products by category")
		.WithDescription("Get all products in the catalog by category");
	}
}