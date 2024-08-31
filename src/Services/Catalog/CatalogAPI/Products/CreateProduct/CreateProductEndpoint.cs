namespace CatalogAPI.Products.CreateProduct;

public record class CreateProductRequest(string Name, List<string> Categories, string Description, string ImageFile, decimal Price);

public record class CreateProductResponse(Guid Id);

public class CreateProductEndpoint
{

}
