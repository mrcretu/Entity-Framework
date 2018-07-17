using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using eShop.Domain;

namespace eShop.DataAccess.Dapper
{
    public class OrderRepository : DapperRepository<Order>
    {
        public OrderRepository() : base("[Order]")
        {
        }

        //Explain how transaction works
        protected override void Insert(Order order, IDbConnection cn)
        {
            var sqlInsertOrder = @"insert into [Order] ([Id],[CustomerId],[PlacedAt],[DeliveredAt],[Amount])
                                values (@Id,@CustomerId,@PlacedAt,@DeliveredAt,@Amount)";
            var sqlInsertProduct = @"INSERT INTO [Product] ([Id],[Name],[Description],[Price])
     VALUES (@Id, @Name, @Description, @Price)";
            var sqlInsertOrderItem = @"INSERT INTO [OrderItem]  ([Id],[OrderId],[ProductId],[Quantity]) 
VALUES  (@Id,@OrderId,@ProductId,@Quantity)";

            using (IDbConnection connection = Connection)
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute(sqlInsertProduct, order.Items[0].Product, transaction: transaction);
                    var parameters = new
                    {
                        Id = order.Id,
                        CustomerId = order.CustomerId,
                        PlacedAt = order.PlacedAt,
                        DeliveredAt = order.DeliveredAt,
                        Amount = order.TotalPrice
                    };
                    connection.Execute(sqlInsertOrder, parameters, transaction: transaction);
                    connection.Execute(sqlInsertOrderItem, order.Items[0], transaction: transaction);
                    transaction.Commit();
                }
            }
        }

        public override ICollection<Order> GetAll()
        {
            return GetAllMultiMapping();
        }

        //Multi mapping with one to many relation
        // Maps single row to several objects
        private List<Order> GetAllMultiMapping()
        {
            using (var cn = Connection)
            {
                cn.Open();

                var sql = @"select * from [Order] fo join [OrderItem] oi on fo.[Id] = oi.[OrderId]";

                var orderDictionary = new Dictionary<Guid, Order>();
                var result = cn.Query<Order, OrderItem, Order>(sql,
                    (order, orderItem) =>
                    {
                        Order fullOrder;

                        if (!orderDictionary.TryGetValue(order.Id, out fullOrder))
                        {
                            // add parrent in dictionary if not exists
                            fullOrder = order;
                            fullOrder.Items = new List<OrderItem>();
                            orderDictionary.Add(fullOrder.Id, fullOrder);
                        }

                        //Add children to parent
                        fullOrder.Items.Add(orderItem);
                        return fullOrder;
                    },
                    splitOn: "OrderId");
                return result.ToList();
            }
        }

        //Multitype
        // Processes multiple grids with a single query
        private List<Order> GetAllMultiType()
        {
            using (var cn = Connection)
            {
                cn.Open();

                var sql = @"select * from FullOrder;
                        select * from OrderItem";

                var orderDictionary = new Dictionary<int, Order>();
                using (var multipleQueryResult = cn.QueryMultiple(sql))
                {
                    var orders = multipleQueryResult.Read<Order>().ToList();
                    var ordersItems = multipleQueryResult.Read<OrderItem>().ToList();
                    foreach (var item in ordersItems) orders.First(o => o.Id == item.Id).Items.Add(item);

                    return orders;
                }
            }
        }

        protected override void Update(Order entity, IDbConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}