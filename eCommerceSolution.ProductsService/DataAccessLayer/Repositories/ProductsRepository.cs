using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.Context;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product?> AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            Product? existingProduct = await _dbContext.Products.FirstOrDefaultAsync(temp => temp.ProductId == productId);
            if (existingProduct != null)
            {
                _dbContext.Products.Remove(existingProduct);
                int affectedRowsCount = await _dbContext.SaveChangesAsync();
                return affectedRowsCount > 0;
            }
            return false;
        }

        public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> condition)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCondition(Expression<Func<Product, bool>> condition)
        {
            return await _dbContext.Products.Where(condition).ToListAsync();
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            Product? existingProduct = await _dbContext.Products.FirstOrDefaultAsync(temp => temp.ProductId.Equals(product.ProductId));
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.ProductId = product.ProductId;
            existingProduct.ProductName = product.ProductName;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.QuantityInStock = product.QuantityInStock;
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
