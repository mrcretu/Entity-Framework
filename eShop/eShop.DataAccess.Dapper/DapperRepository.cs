using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using eShop.Domain;
using eShop.Domain.Repositories;

namespace eShop.DataAccess.Dapper
{
    public abstract class DapperRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        private const string DBConnectionString =
            @"Server=DTPR003274; Database=Dapper;Integrated Security=True";

        private readonly string _tableName;

        public DapperRepository(string tableName)
        {
            _tableName = tableName;
        }

        internal IDbConnection Connection => new SqlConnection(DBConnectionString);

        //Anonymus parameters
        public virtual TEntity GetById(Guid id)
        {
            var item = default(TEntity);

            using (var cn = Connection)
            {
                cn.Open();
                item = cn.QuerySingleOrDefault<TEntity>("SELECT * FROM " + _tableName + " WHERE ID=@ID", new {ID = id});
            }

            return item;
        }

        public abstract ICollection<TEntity> GetAll();

        public virtual ICollection<TEntity> Find(Func<TEntity, bool> filter)
        {
            throw new NotImplementedException();
        }

        public void Create(TEntity entity)
        {
            using (var cn = Connection)
            {
                cn.Open();
                Insert(entity, cn);
            }
        }

        public void Update(TEntity entity)
        {
            using (var cn = Connection)
            {
                cn.Open();
                Update(entity, cn);
            }
        }

        public void Delete(Guid id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                var sql = "DELETE FROM " + _tableName + " WHERE ID=@ID";
                cn.Execute(sql, new {ID = id});
            }
        }

        public List<TEntity> GetAllDefault()
        {
            List<TEntity> items = null;
            using (var cn = Connection)
            {
                cn.Open();
                items = cn.Query<TEntity>("SELECT * FROM " + _tableName).ToList();
            }

            return items;
        }

        protected abstract void Insert(TEntity entity, IDbConnection connection);

        protected abstract void Update(TEntity entity, IDbConnection connection);
    }
}