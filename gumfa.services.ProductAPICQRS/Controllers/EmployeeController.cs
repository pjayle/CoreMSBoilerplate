using AutoMapper;
using Azure;
using gumfa.services.EmployeAPICQRS.Data.Mediator;
using gumfa.services.ProductAPICQRS.Models;
using gumfa.services.ProductAPICQRS.Models.DTO;
using gumfa.services.ProductAPICQRS.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace gumfa.services.EmployeeAPICQRS.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _empMediator;
        private readonly IMapper _mapper;
        private APIResponseDto _response;
        public EmployeeController(IConfiguration configuration, IMediator mediator, IMapper mapper)
        {
            this._configuration = configuration;
            this._empMediator = mediator;
            this._mapper = mapper;
            _response = new APIResponseDto();
        }

        [HttpGet]
        [Authorize(Roles = "OPERATOR")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _response.Result = await _empMediator.Send(new GetEmployeeListQuery());
                _response.Message = "SUCCESS";
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
                _response.Result = await _empMediator.Send(new GetEmployeeByIdQuery() { Id = id });
                _response.Message = "SUCCESS";
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
        public async Task<IActionResult> Post([FromBody] Employee EmployeeaddDto)
        {
            try
            {
                Employee Employee = _mapper.Map<Employee>(EmployeeaddDto);
                _response.Result = await _empMediator.Send(new CreateEmployeeCommand(Employee.Name, Employee.EMPCode));
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
        public async Task<IActionResult> Put([FromBody] Employee EmployeeUpdateDto)
        {
            try
            {
                Employee Employee = _mapper.Map<Employee>(EmployeeUpdateDto);
                _response.Result = await _empMediator.Send(new UpdateEmployeeCommand(Employee.EmployeeID, Employee.Name, Employee.EMPCode));
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

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _response.Result = await _empMediator.Send(new DeleteEmployeeCommand() { Id = id });
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
    }
}
