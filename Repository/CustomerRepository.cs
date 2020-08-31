using Dapper;
using Microsoft.AspNetCore.Mvc;
using OData.Dapper.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Dapper.Api.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string connectionString;

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //CREATE CUSTOMERS
        public async Task<bool> CreateCustomer(Customer customer)
        {
            int rowsAffected = 0;
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"INSERT INTO Customers
                  (CustomerID
                  ,CompanyName
                  ,ContactName
                  ,ContactTitle
                  ,Address
                  ,City
                  ,Region
                  ,PostalCode
                  ,Country
                  ,Phone
                  ,Fax)
            VALUES
                  (CustomerID
                  ,CompanyName
                  ,<ContactName
                  ,ContactTitle
                  ,Address
                  ,City
                  ,Region
                  ,PostalCode
                  ,Country
                  ,Phone
                  ,Fax)";
                    rowsAffected = await sqlConnection.ExecuteAsync(sqlQuery,
                        new
                        {
                            customer.CompanyName,
                            customer.ContactName,
                            customer.ContactTitle,
                            customer.Address,
                            customer.City,
                            customer.Region,
                            customer.PostalCode,
                            customer.Country,
                            customer.Phone,
                            customer.Fax
                        });
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> DeleteCustomer(string customerID)
        {
            throw new NotImplementedException();
        }
        //GET All Customers
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            IEnumerable<Customer> customers = null;
            try
            {

                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"SELECT CustomerID
      ,CompanyName
      ,ContactName
      ,ContactTitle
      ,Address
      ,City
      ,Region
      ,PostalCode
      ,Country
      ,Phone
      ,Fax
  FROM Customers";
                    customers = await sqlConnection.QueryAsync<Customer>(sqlQuery);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());
            }
            return customers;
        }
        //UPDATE CUSTOMERS
        public async Task<bool> UpdateCustomer(Customer customer)
        {
            int rowsAffected = 0;
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"UPDATE Customers
   SET CompanyName = @CompanyName
      ,ContactName = @ContactName
      ,ContactTitle = @ContactTitle
      ,Address = @Address
      ,City = @City
      ,Region = @Region
      ,PostalCode = @PostalCode
      ,Country = @Country
      ,Phone = @Phone
      ,Fax = @Fax
 WHERE CustomerID = @CustomerID;";
                    rowsAffected = await sqlConnection.ExecuteAsync(sqlQuery);
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
