using AutoMapper;
using Azure;
using gumfa.services.MasterAPI.Models;
using gumfa.services.MasterAPI.Models.DTO;
using gumfa.services.MasterAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace gumfa.services.MasterAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private APIResponseDto _response;
        public ProductController(IConfiguration configuration, IProductService productService, IMapper mapper)
        {
            this._configuration = configuration;
            this._productService = productService;
            this._mapper = mapper;
            _response = new APIResponseDto();
        }

        [HttpGet]
        [Authorize(Roles = "OPERATOR")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.getall();
                _response.Result = _mapper.Map<IEnumerable<ProductListDto>>(products);
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
                Product obj = await _productService.getbyid(id);
                _response.Result = _mapper.Map<ProductUpdateDto>(obj);
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
        public async Task<IActionResult> Post([FromBody] ProductAddDto ProductaddDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(ProductaddDto);
                _response.Result = await _productService.add(product);
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
        public async Task<IActionResult> Put([FromBody] ProductUpdateDto productUpdateDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productUpdateDto);
                _response.Result = await _productService.update(product);
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
                _response.Result = await _productService.delete(id);
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
