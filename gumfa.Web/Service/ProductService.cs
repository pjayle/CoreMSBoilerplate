using gumfa.Web.Models;
using gumfa.Web.Models.DTO;
using gumfa.Web.Utility;

namespace gumfa.Web.Service
{
    public interface IProductService
    {
        Task<APIResponseDto?> GetAllProductsAsync();
        Task<APIResponseDto?> CreateProductsAsync(ProductAddDto productDto);
        Task<APIResponseDto?> GetProductByIdAsync(int id);
        Task<APIResponseDto?> UpdateProductsAsync(ProductUpdateDto productDto);
        Task<APIResponseDto?> DeleteProductsAsync(int id);
    }
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.GET,
                Url = CONST.ProductAPIBase + "/api/product"
            });
        }

        public async Task<APIResponseDto?> CreateProductsAsync(ProductAddDto productDto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.POST,
                Data = productDto,
                Url = CONST.ProductAPIBase + "/api/product"
            });
        }

        public async Task<APIResponseDto?> DeleteProductsAsync(int id)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.DELETE,
                Url = CONST.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<APIResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.GET,
                Url = CONST.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<APIResponseDto?> UpdateProductsAsync(ProductUpdateDto productDto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.PUT,
                Data = productDto,
                Url = CONST.ProductAPIBase + "/api/product",
                ContentType = CONST.ContentType.Json
            });
        }
    }
}
