using BusinessLogicLayer.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.ProductsService.BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            //Add Business logic layer services to IoC container
            services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
            return services;
        }
    }
}
