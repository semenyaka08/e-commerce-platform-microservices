using Catalog.Api.Products.CreateProduct;
using Catalog.Api.Products.GetProductById;

namespace Catalog.Api.Products;

public static class ProductMapper
{
    public static CreateProductCommand FromRequestToCommand(CreateProductRequest dto) 
        => new CreateProductCommand(dto.Name, dto.Description, dto.ImageFile, dto.Price, dto.Category);

    public static CreateProductResponse FromResultToResponse(CreateProductResult result)
        => new CreateProductResponse(result.Id);
}