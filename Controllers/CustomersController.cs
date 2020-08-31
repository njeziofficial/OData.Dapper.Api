using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OData.Dapper.Api.Models;
using OData.Dapper.Api.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OData.Dapper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await customerRepository.GetCustomers());
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        //public string Get(int id)
        //{
            

        //}

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            var createdCustomer = await customerRepository.CreateCustomer(customer);
            if(createdCustomer == true)
            {
                return Ok(createdCustomer);
            }
            return BadRequest("There is an error in posting the object");
        }


        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string customerID, [FromBody] Customer customer)
        {
            customer.CustomerID = customerID;
            return Ok(await customerRepository.UpdateCustomer(customer));
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
