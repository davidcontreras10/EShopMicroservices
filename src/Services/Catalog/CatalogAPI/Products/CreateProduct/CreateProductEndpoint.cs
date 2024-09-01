namespace CatalogAPI.Products.CreateProduct;

public record class CreateProductRequest(string Name, List<string> Categories, string Description, string ImageFile, decimal Price);

public record class CreateProductResponse(Guid Id);

public class CreateProductEndpoint : CarterModule
{
	public override void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
		{
			var command = request.Adapt<CreateProductCommand>();
			var result = await sender.Send(command);
			var response = result.Adapt<CreateProductResponse>();
			return Results.Created($"/products/{response.Id}", response);
		})
		.WithName("CreateProduct")
		.Produces<CreateProductResponse>(StatusCodes.Status201Created, "application/json")
		.ProducesProblem(StatusCodes.Status400BadRequest, "application/json")
		.WithSummary("Create a new product")
		.WithDescription("Create a new product in the catalog");
	}
}


//var request = await req.BindAndValidate<CreateProductRequest>();
//			if (!request.ValidationResult.IsValid)
//			{
//				res.StatusCode = 400;
//				await res.Negotiate(request.ValidationResult.GetFormattedErrors());
//				return;
//			}

//			var command = new CreateProductCommand(request.Name, request.Categories, request.Description, request.ImageFile, request.Price);
//var result = await Mediator.Send(command);

//await res.Negotiate(result);