using Dapper;
using OData.Dapper.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OData.Dapper.Api.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly string connectionString;

        public SupplierRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //CREATE Supplier
        public async Task<bool> CreateSupplier(Supplier supplier)
        {
            int rowsAffected = 0;
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"INSERT INTO Suppliers
                  (CompanyName,
                  ContactName,
                  ContactTitle,
                  Address,
                  City,
                  Region,
                  PostalCode,
                  Country,
                  Phone,
                  Fax,
                  HomePage)
            VALUES
                  (CompanyName,
                  ContactName,
                  ContactTitle,
                  Address,
                  City,
                  Region,
                  PostalCode,
                  Country,
                  Phone,
                  Fax,
                   HomePage)";
                    rowsAffected = await sqlConnection.ExecuteAsync(sqlQuery,
                    new
                    {
                        supplier.CompanyName,
                        supplier.ContactName,
                        supplier.ContactTitle,
                        supplier.Address,
                        supplier.City,
                        supplier.Region,
                        supplier.PostalCode,
                        supplier.Country,
                        supplier.Phone,
                        supplier.Fax,
                        supplier.HomePage
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
        //GET All Suppliers
        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = null;
            try
            {

                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"SELECT SupplierID
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
,HomePage
  FROM Suppliers";
                    suppliers = await sqlConnection.QueryAsync<Supplier>(sqlQuery);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());
            }
            return suppliers;
        }
        //GET Supplier by ID
        public async Task<Supplier> GetSupplierById(int id)
        {
            Supplier supplier = new Supplier();
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"SELECT SupplierID
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
    ,HomePage
  FROM Suppliers WHERE SupplierID = @SupplierID";
                    IEnumerable<Supplier> suppliers = await sqlConnection.QueryAsync<Supplier>(sqlQuery, new { SupplierID = id });
                    supplier = suppliers.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());
            }
            return supplier;
        }
        //UPDATE Supplier
        public async Task<bool> UpdateSupplier(Supplier supplier)
        {
            bool response = false;
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"UPDATE [dbo].[Suppliers]
   SET [CompanyName] = @CompanyName,
      [ContactName] = @ContactName,
      [ContactTitle] = @ContactTitle,
      [Address] = @Address,
      [City] = @City,
      [Region] = @Region,
      [PostalCode] = @PostalCode,
      [Country] = @Country,
      [Phone] = @Phone,
      [Fax] = @Fax,
      [HomePage] = @HomePage
 WHERE SupplierID= @SupplierID";
                    int rowsAffected = await sqlConnection.ExecuteAsync(sqlQuery,
                        new
                        {
                            supplier.SupplierID,
                            supplier.CompanyName,
                            supplier.ContactName,
                            supplier.ContactTitle,
                            supplier.Address,
                            supplier.City,
                            supplier.Region,
                            supplier.PostalCode,
                            supplier.Country,
                            supplier.Phone,
                            supplier.Fax,
                            supplier.HomePage
                        });
                    if (rowsAffected > 0)
                    {
                        response = true;
                    }
                    response = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return response;
        }
        //DELETE Supplier
        public async Task<bool> Delete(int id)
        {
            bool response = false;
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"DELETE FROM Suppliers WHERE SupplierID = @SupplierID;";
                    int rowsAffected = await sqlConnection.ExecuteAsync(sqlQuery, new { SupplierID = id });
                    if (rowsAffected > 0)
                    {
                        response = true;
                    }
                    response = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }
    }
}
