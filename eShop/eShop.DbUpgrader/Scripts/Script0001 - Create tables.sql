create table [Customer] (
 [Id] uniqueidentifier primary key,
 [FirstName] varchar(100) not null,
 [LastName] varchar(100) not null,
 [Age] int not null);

 create table [Order] (
                 [Id] uniqueidentifier primary key ,
                 [CustomerId] uniqueidentifier not null,
                 [PlacedAt] datetime2(7) not null,
				 [DeliveredAt] datetime2(7) null,
                 [Amount] decimal(6,2),
                  CONSTRAINT fk_order_customer_id FOREIGN KEY ([CustomerId]) REFERENCES Customer (Id));

create table [Product] (
                 [Id] uniqueidentifier primary key ,
                 [Description] varchar(100) not null,
                 [Price] decimal(6,2));

create table [OrderItem] (
                 [Id] uniqueidentifier primary key ,
                 [OrderId] uniqueidentifier not null,
                 [ProductId] uniqueidentifier not null,
                 [Quantity] int not null,
                  CONSTRAINT fk_orderItem_order_id FOREIGN KEY ([OrderId]) REFERENCES [Order] ([Id]),
CONSTRAINT fk_orderItem_product_id FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]));