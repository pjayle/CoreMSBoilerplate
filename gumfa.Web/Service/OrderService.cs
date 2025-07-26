using gumfa.Web.Models;
using gumfa.Web.Models.DTO;
using gumfa.Web.Utility;

namespace gumfa.Web.Service
{
    public interface IOrderService
    {
        Task<APIResponseDto?> GetAllOrdersAsync();
        Task<APIResponseDto?> CreateOrdersAsync(OrderAddDto OrderDto);
        Task<APIResponseDto?> GetOrderByIdAsync(int id);
        Task<APIResponseDto?> UpdateOrdersAsync(OrderUpdateDto OrderDto);
        Task<APIResponseDto?> DeleteOrdersAsync(int id);
    }
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;
        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> GetAllOrdersAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.GET,
                Url = CONST.OrderAPIBase + "/api/order"
            });
        }

        public async Task<APIResponseDto?> CreateOrdersAsync(OrderAddDto OrderDto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.POST,
                Data = OrderDto,
                Url = CONST.OrderAPIBase + "/api/order"
            });
        }

        public async Task<APIResponseDto?> DeleteOrdersAsync(int id)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.DELETE,
                Url = CONST.OrderAPIBase + "/api/order/" + id
            });
        }

        public async Task<APIResponseDto?> GetOrderByIdAsync(int id)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.GET,
                Url = CONST.OrderAPIBase + "/api/order/" + id
            });
        }

        public async Task<APIResponseDto?> UpdateOrdersAsync(OrderUpdateDto OrderDto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = CONST.ApiType.PUT,
                Data = OrderDto,
                Url = CONST.OrderAPIBase + "/api/order",
                ContentType = CONST.ContentType.Json
            });
        }
    }
}
