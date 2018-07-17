using System.Collections.Generic;
using System.Data;
using Dapper;
using eShop.Domain;

namespace eShop.DataAccess.Dapper
{
    public class ProductRepository : DapperRepository<Product>
    {
        private const string InsertSql = @"INSERT INTO [Product] ([Id],[Name],[Description],[Price])
                                            values (@Id, @FirstName, @LastName, @Age)";

        public ProductRepository() : base("Product")
        {
        }

        public override ICollection<Product> GetAll()
        {
            return GetAllDefault();
        }

        protected override void Insert(Product entity, IDbConnection connection)
        {
        }

        protected override void Update(Product entity, IDbConnection connection)
        {
            connection.Open();
            var sql = @"update Product
                        set [Name] = @Name
                        where[Id] = @Id";
            connection.Execute(sql, connection);
        }
    }
}