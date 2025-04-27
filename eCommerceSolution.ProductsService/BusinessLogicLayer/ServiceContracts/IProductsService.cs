using DataAccessLayer.Entities;
using eCommerce.BusinessLogicLayer.DTO;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.ServiceContracts
{
    public interface IProductsService
    {
        Task<List<ProductResponse?>> GetProducts();
        Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<ProductResponse, bool>> condition);
        Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> condition);
        Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);
        Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);
        Task<bool> DeleteProduct(Guid productId);
    }
}
