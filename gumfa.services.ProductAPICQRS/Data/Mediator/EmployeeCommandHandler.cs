using gumfa.services.ProductAPICQRS.Models;
using gumfa.services.ProductAPICQRS.Service;
using MediatR;

namespace gumfa.services.EmployeAPICQRS.Data.Mediator
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly IEmployeeService _employeeService;
        public CreateEmployeeHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = new Employee
            {
                Name = request.Name,
                EMPCode = request.EMPCode,
            };
            return await _employeeService.add(employee);
        }
    }
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
    {
        private readonly IEmployeeService _employeeService;
        public UpdateEmployeeHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = await _employeeService.getbyid(request.Id);
            employee.EmployeeID = request.Id;
            employee.Name = request.Name;
            employee.EMPCode = request.EMPCode;

            return await _employeeService.update(employee);
        }
    }

    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Employee>
    {
        private readonly IEmployeeService _employeeService;
        public DeleteEmployeeHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<Employee> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _employeeService.delete(request.Id);
        }
    }
}
