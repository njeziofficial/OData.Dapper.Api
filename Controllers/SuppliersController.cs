using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OData.Dapper.Api.Models;
using OData.Dapper.Api.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OData.Dapper.Api.Controllers
{    
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository repository;

        public SuppliersController(ISupplierRepository repository)
        {
            this.repository = repository;
        }
        // GET: api/<SuppliersController>
        [HttpGet]
        [EnableQuery(PageSize = 5)]
        public async Task<IActionResult> Get() =>
            Ok(await repository.GetSuppliers());

        // GET api/<SuppliersController>/5
        [HttpGet("{id}")]
        [EnableQuery()]
        public async Task<IActionResult> GetSuppierById(int id) =>
            Ok(await repository.GetSupplierById(id));

        // POST api/<CustomersController>
        [HttpPost]
        [EnableQuery()]
        public async Task<IActionResult> Post([FromBody] Supplier supplier) =>
            Ok(await repository.CreateSupplier(supplier));

        // PUT api/<SuppliersController>/5
        [HttpPut("{id}")]
        [EnableQuery()]
        public async Task Put(int id, [FromBody] Supplier supplier)
        {
            supplier.SupplierID = id;
          Ok(await repository.UpdateSupplier(supplier));
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        [EnableQuery()]
        public async Task<IActionResult> Delete(int id) => Ok(await repository.Delete(id));
    }
}
