using gumfa.services.ProductAPICQRS.Models;
using gumfa.services.ProductAPICQRS.Service;
using MediatR;

namespace gumfa.services.EmployeAPICQRS.Data.Mediator
{
    public class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, List<Employee>>
    {
        private readonly IEmployeeService _employeeService;
        public GetEmployeeListHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<List<Employee>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return await _employeeService.getall();
        }
    }

    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeService _employeeService;
        public GetEmployeeByIdHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _employeeService.getbyid(request.Id);
        }
    }
}
