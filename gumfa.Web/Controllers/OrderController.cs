using gumfa.Web.Models;
using gumfa.Web.Models.DTO;
using gumfa.Web.Service;
using gumfa.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace gumfa.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _OrderService;
        public OrderController(IOrderService OrderService)
        {
            _OrderService = OrderService;
        }

        public async Task<IActionResult> Index()
        {
            //List<OrderListDto>? list = new();

            //APIResponseDto? response = await _OrderService.GetAllOrdersAsync();

            //if (response != null && response.IsSuccess)
            //{
            //    list = JsonConvert.DeserializeObject<List<OrderListDto>>(Convert.ToString(response.Result));
            //}
            //else
            //{
            //    TempData["error"] = response?.Message;
            //}

            //return View(list);

            return View();
        }

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderListDto> list;
            string userId = "";
            if (!User.IsInRole(CONST.RoleAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            }
            APIResponseDto response = _OrderService.GetAllOrdersAsync().GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderListDto>>(Convert.ToString(response.Result));
                switch (status)
                {
                    case "created":
                        list = list.Where(u => u.Status == CONST.ORDER_Status_Created.ToLower());
                        break;
                    case "approved":
                        list = list.Where(u => u.Status == CONST.ORDER_Status_Approved.ToLower());
                        break;
                    case "readyforpickup":
                        list = list.Where(u => u.Status == CONST.ORDER_Status_ReadyForPickup.ToLower());
                        break;
                    case "cancelled":
                        list = list.Where(u => u.Status == CONST.ORDER_Status_Cancelled.ToLower() || u.Status == CONST.ORDER_Status_Refunded.ToLower());
                        break;
                    default:
                        break;
                }
            }
            else
            {
                list = new List<OrderListDto>();
            }
            return Json(new { data = list.OrderByDescending(u => u.OrderID) });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderAddDto model)
        {
            if (ModelState.IsValid)
            {

                model.Status = "created";
                APIResponseDto? response = await _OrderService.CreateOrdersAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Order created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int OrderId)
        {
            APIResponseDto? response = await _OrderService.GetOrderByIdAsync(OrderId);

            if (response != null && response.IsSuccess)
            {
                OrderUpdateDto? model = JsonConvert.DeserializeObject<OrderUpdateDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(OrderUpdateDto OrderDto)
        {
            APIResponseDto? response = await _OrderService.DeleteOrdersAsync(OrderDto.OrderID);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Order deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(OrderDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int OrderId)
        {
            APIResponseDto? response = await _OrderService.GetOrderByIdAsync(OrderId);

            if (response != null && response.IsSuccess)
            {
                OrderUpdateDto? model = JsonConvert.DeserializeObject<OrderUpdateDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderUpdateDto OrderDto)
        {
            if (ModelState.IsValid)
            {
                APIResponseDto? response = await _OrderService.UpdateOrdersAsync(OrderDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Order updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(OrderDto);
        }

    }
}
