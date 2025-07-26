using gumfa.Web.Models;
using gumfa.Web.Models.DTO;
using gumfa.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            List<OrderListDto>? list = new();

            APIResponseDto? response = await _OrderService.GetAllOrdersAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderListDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
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
