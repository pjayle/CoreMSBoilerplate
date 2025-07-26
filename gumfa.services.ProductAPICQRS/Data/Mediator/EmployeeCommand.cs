using gumfa.services.ProductAPICQRS.Models;
using MediatR;

namespace gumfa.services.EmployeAPICQRS.Data.Mediator
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public CreateEmployeeCommand(string name, string empcode)
        {
            Name = name;
            EMPCode = empcode;
        }
        public string EMPCode { get; set; }
        public string Name { get; set; }
    }

    public class UpdateEmployeeCommand : IRequest<Employee>
    {
        public UpdateEmployeeCommand(int id, string name, string empcode)
        {
            Id = id;
            Name = name;
            EMPCode = empcode;
        }
        public int Id { get; set; }
        public string EMPCode { get; set; }
        public string Name { get; set; }
    }

    public class DeleteEmployeeCommand : IRequest<Employee>
    {
        public int Id { get; set; }
    }
}
