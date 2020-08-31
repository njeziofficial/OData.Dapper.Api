using OData.Dapper.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OData.Dapper.Api.Repository
{
    public interface ISupplierRepository
    {
        Task<bool> CreateSupplier(Supplier supplier);
        Task<IEnumerable<Supplier>> GetSuppliers();

        Task<Supplier> GetSupplierById(int id);

        Task<bool> UpdateSupplier(Supplier supplier);
        Task<bool> Delete(int id);
    }
}