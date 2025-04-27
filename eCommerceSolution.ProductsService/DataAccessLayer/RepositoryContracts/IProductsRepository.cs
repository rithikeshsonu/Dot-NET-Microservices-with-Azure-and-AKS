using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.RepositoryContracts
{
    /// <summary>
    /// /Represents a repository for managing 'Products' table
    /// </summary>
    public interface IProductsRepository
    {
        /// <summary>
        /// Retrieves all products asynchronously
        /// </summary>
        /// <returns>Returns all products from table</returns>
        Task<IEnumerable<Product>> GetProducts();
        /// <summary>
        /// Retrieves products based on specific condition asynchronously
        /// </summary>
        /// <param name="condition">Condition to filter products</param>
        /// <returns>Returns a collection of matching records</returns>
        Task<IEnumerable<Product>> GetProductsByCondition(Expression<Func<Product, bool>> condition);
        /// <summary>
        /// Retrieves a single product based on specific condition asynchronously
        /// </summary>
        /// <param name="condition">Condition to filter the product</param>
        /// <returns>Returns a single product or null if not fount</returns>
        Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> condition);
        /// <summary>
        /// Adds a new product to Products table asynchronously
        /// </summary>
        /// <param name="product">Product to be added</param>
        /// <returns>Returns the same product instance that was added or null if not successful</returns>
        Task<Product?> AddProduct(Product product);
        /// <summary>
        /// Updates an existing product asynchronously
        /// </summary>
        /// <param name="product">The product to be updated</param>
        /// <returns>Returns the product instance that was updated or null if update fails</returns>
        Task<Product?> UpdateProduct(Product product);
        /// <summary>
        /// Deletes a product asynchronously
        /// </summary>
        /// <param name="productId">Expects product Id to be deleted</param>
        /// <returns>Returns true if deleted successfully</returns>
        Task<bool> DeleteProduct(Guid productId);
    }
}
