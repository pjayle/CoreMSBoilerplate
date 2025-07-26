using gumfa.services.ProductAPICQRS.Models;
using MediatR;

namespace gumfa.services.EmployeAPICQRS.Data.Mediator
{
    public class GetEmployeeListQuery : IRequest<List<Employee>>
    {
    }
    public class GetEmployeeByIdQuery : IRequest<Employee>
    {
        public int Id { get; set; }
    }
}
