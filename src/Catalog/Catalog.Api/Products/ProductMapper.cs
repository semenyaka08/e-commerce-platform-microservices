using Catalog.Api.Products.CreateProduct;
using Catalog.Api.Products.UpdateProduct;

namespace Catalog.Api.Products;

public static class ProductMapper
{
    public static CreateProductCommand FromRequestToCommand(CreateProductRequest dto) 
        => new CreateProductCommand(dto.Name, dto.Description, dto.ImageFile, dto.Price, dto.Category);

    public static CreateProductResponse FromResultToResponse(CreateProductResult result)
        => new CreateProductResponse(result.Id);
    
    public static UpdateProductCommand FromRequestToCommand(UpdateProductRequest dto) 
        => new UpdateProductCommand(dto.Id, dto.Name, dto.Description, dto.ImageFile, dto.Price, dto.Category);
}