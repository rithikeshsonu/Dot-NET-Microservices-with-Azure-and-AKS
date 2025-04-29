using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;

namespace eCommerce.ProductsService.API.APIEndpoints
{
    public static class ProductAPIEndpoints
    {
        public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
        {
            //GET /api/products
            app.MapGet("/api/products", async(IProductsService productsService) =>
            {
                List<ProductResponse?> products = await productsService.GetProducts();
                return Results.Ok(products);
            });

            //GET /api/products/search/productId/0000000-00000-000000-00000-00000000
            app.MapGet("/api/products/search/productId/{productId}", async (IProductsService productsService, Guid productId) =>
            {
                ProductResponse? product = await productsService.GetProductByCondition(temp => temp.ProductId.Equals(productId));
                return Results.Ok(product);
            });

            //GET /api/products/search/search/{searchString}
            app.MapGet("/api/products/search/{searchString}", async (IProductsService productsService, string searchString) =>
            {
                List<ProductResponse?> productsByProductName = await productsService.GetProductsByCondition(temp => temp.ProductName != null && temp.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase));

                List<ProductResponse?> productsByProductId = await productsService.GetProductsByCondition(temp => temp.Category != null && temp.Category.Contains(searchString, StringComparison.OrdinalIgnoreCase));

                var products = productsByProductName.Union(productsByProductId);
                return Results.Ok(products);
            });

            //POST /api/products
            app.MapPost("/api/products", async (IProductsService productsService, IValidator<ProductAddRequest> productAddRequestValidator , ProductAddRequest productAddRequest) =>
            {
                ValidationResult validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors
                                                            .GroupBy(temp => temp.PropertyName)
                                                                .ToDictionary(grp => grp.Key, 
                                                                    grp => grp.Select(err => err.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }
                ProductResponse? addedProductResponse = await productsService.AddProduct(productAddRequest);
                if(addedProductResponse != null)
                    return Results.Created($"/api/products/search/productId/{addedProductResponse.ProductId}", addedProductResponse);
                else
                    return Results.Problem("Error in adding product");
            });

            //PUT /api/products
            app.MapPut("/api/products", async (IProductsService productsService, IValidator<ProductUpdateRequest> productUpdateRequestValidator, ProductUpdateRequest productUpdateRequest) =>
            {
                ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errors = validationResult.Errors
                                                            .GroupBy(temp => temp.PropertyName)
                                                                .ToDictionary(grp => grp.Key,
                                                                    grp => grp.Select(err => err.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }
                ProductResponse? updatedProductResponse = await productsService.UpdateProduct(productUpdateRequest);
                if (updatedProductResponse != null)
                    return Results.Ok(updatedProductResponse);
                else
                    return Results.Problem("Error in updating product");
            });

            //DELETE api/products/xxxxxxxxxxxxxxxxxx
            app.MapDelete("api/products/{productId:guid}", async (IProductsService productsService, Guid productId) =>
            {
                bool isDeleted = await productsService.DeleteProduct(productId);
                if (isDeleted)
                    return Results.Ok(true);
                else
                    return Results.Problem("Error in deleting product");
            });


            return app;
        }
    }
}
