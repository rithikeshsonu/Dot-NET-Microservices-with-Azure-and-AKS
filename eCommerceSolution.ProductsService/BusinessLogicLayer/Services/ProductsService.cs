using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
        private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsRepository;
        public ProductsService(IValidator<ProductAddRequest> productAddRequestValidator, IValidator<ProductUpdateRequest> productUpdateRequestValidator, IMapper mapper, IProductsRepository productsRepository)
        {
            _productAddRequestValidator = productAddRequestValidator;
            _productUpdateRequestValidator = productUpdateRequestValidator;
            _mapper = mapper;
            _productsRepository = productsRepository;
        }
        public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
        {
            if(productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest));
            }
            ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage)); //Error1, Error2, Error3, .....
                throw new ArgumentException(errors);
            }
            Product productInput = _mapper.Map<Product>(productAddRequest);
            Product? addedProduct = await _productsRepository.AddProduct(productInput);
            if(addedProduct == null)
            {
                return null;
            }
            ProductResponse? addedProductResponse = _mapper.Map<ProductResponse>(addedProduct);
            return addedProductResponse;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            Product? existingProduct = await _productsRepository.GetProductByCondition(temp => temp.ProductId.Equals(productId));
            if (existingProduct == null) return false;
            bool isDeleted = await _productsRepository.DeleteProduct(productId);
            return isDeleted;
        }

        public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> condition)
        {
            Product? product = await _productsRepository.GetProductByCondition(condition);
            if (product == null)
            {
                return null;
            }
            ProductResponse productResponse = _mapper.Map<ProductResponse>(product); //ProductToProductResponseMappingProfile is invoked
            return productResponse;

        }

        public async Task<List<ProductResponse?>> GetProducts()
        {
            IEnumerable<Product?> products = await _productsRepository.GetProducts();
            IEnumerable<ProductResponse?> productsResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return productsResponse.ToList();
        }

        public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> condition)
        {
            IEnumerable<Product?> products = await _productsRepository.GetProductsByCondition(condition);
            IEnumerable<ProductResponse?> productsResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return productsResponse.ToList();
        }

        public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            Product? existingProduct = await _productsRepository.GetProductByCondition(temp => temp.ProductId.Equals(productUpdateRequest.ProductId));

            if (existingProduct == null)
            {
                throw new ArgumentException("Invalid Product Id");
            }
            ValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage));
                throw new ArgumentException(errors);
            }
            Product product = _mapper.Map<Product>(productUpdateRequest); //Invokes ProductUpdateRequestToProductMappingProfile
            Product? updatedProduct = await _productsRepository.UpdateProduct(product);
            ProductResponse? updatedProductResponse = _mapper.Map<ProductResponse>(updatedProduct);
            return updatedProductResponse;
            
        }
    }
}
