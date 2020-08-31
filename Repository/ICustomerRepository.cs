using OData.Dapper.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OData.Dapper.Api.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> CreateCustomer(Customer customer);
        Task<bool> DeleteCustomer(string customerID);


    }
}
