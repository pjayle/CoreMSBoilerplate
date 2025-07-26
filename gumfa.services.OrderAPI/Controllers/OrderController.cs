using AutoMapper;
using gumfa.services.OrderAPI.Models;
using gumfa.services.OrderAPI.Models.DTO;
using gumfa.services.OrderAPI.Service;

//using gumfa.services.OrderAPI.Models;
//using gumfa.services.OrderAPI.Models.DTO;
//using gumfa.services.OrderAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace gumfa.services.OrderAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private APIResponseDto _response;
        public OrderController(IConfiguration configuration, IOrderService orderService, IMapper mapper)
        {
            this._configuration = configuration;
            this._orderService = orderService;
            this._mapper = mapper;
            _response = new APIResponseDto();
        }

        [HttpGet]
        [Authorize(Roles = "OPERATOR")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orders = await _orderService.getall();
                _response.Result = _mapper.Map<IEnumerable<OrderListDto>>(orders);
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "");

                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Order obj = await _orderService.getbyid(id);
                _response.Result = _mapper.Map<OrderUpdateDto>(obj);
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "");

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Post([FromBody] OrderAddDto orderAddDto)
        {
            try
            {
                Order order = _mapper.Map<Order>(orderAddDto);
                order.OrderBy = 1;
                order.OrderOn = DateTime.Now;
                order.DeliveryDate = DateTime.Now.AddDays(5);

                _response.Result = await _orderService.add(order);
                _response.Message = "ADD SUCCESS";
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "");
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPut()]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Put([FromBody] OrderUpdateDto orderUpdateDto)
        {
            try
            {
                Order order = _mapper.Map<Order>(orderUpdateDto);
                _response.Result = await _orderService.update(order);
                _response.Message = "UPDATE SUCCESS";
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "");

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _response.Result = await _orderService.delete(id);
                _response.Message = "DELETE SUCCESS";
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "");

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }
    }
}
