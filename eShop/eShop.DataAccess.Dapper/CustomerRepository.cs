using System.Collections.Generic;
using System.Data;
using Dapper;
using eShop.Domain;

namespace eShop.DataAccess.Dapper
{
    public class CustomerRepository : DapperRepository<Customer>
    {
        private const string InsertSql = @"INSERT INTO [Customer] ([Id],[FirstName],[LastName],[Age])
                                            values (@Id, @FirstName, @LastName, @Age)";

        public CustomerRepository() : base("[Customer]")
        {
        }

        public override ICollection<Customer> GetAll()
        {
            return GetAllDefault();
        }

        //StronglyTyped
        protected override void Insert(Customer customer, IDbConnection connection)
        {
            InsertStronglyTyped(customer, connection);
            //InsertWithAnonymusParameters(customer, connection);
        }

        private void InsertStronglyTyped(Customer customer, IDbConnection connection)
        {
            connection.Execute(InsertSql, customer);
        }

        private void InsertWithAnonymusParameters(Customer customer, IDbConnection connection)
        {
            var parameters = new[]
            {
                new {Id = 2, FirstName = "NumeAnonymus", LastName = "adressAnonymus", Age = 7}
            };
            connection.Execute(InsertSql, parameters);
        }

        protected override void Update(Customer customer, IDbConnection connection)
        {
            var sql = @"Update Customer 
                Set FirstName = @FirstName
                Where Id = @Id";
            var parameters = new[]
            {
                new {customer.Id, customer.FirstName}
            };
            connection.Execute(sql, parameters);
        }

        public void CreateTable()
        {
            using (var con = Connection)
            {
                con.Open();
                var sql = @"create table [Customer] (
                         [Id] uniqueidentifier primary key,
                         [FirstName] varchar(100) not null,
                         [LastName] varchar(100) not null,
                         [Age] int not null)";
                con.Execute(sql);
                con.Close();
            }
        }
    }
}