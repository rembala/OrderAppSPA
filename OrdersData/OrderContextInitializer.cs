using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersEntities.Entities;

namespace OrdersData
{
    public class OrderContextInitializer : CreateDatabaseIfNotExists<OrdersContext>
    {
        protected override void Seed(OrdersContext context)
        {
            context.User.Add(
                 new User()
                 {
                     UserID = 1,
                     Email = "maslovskij.artur@gmail.com",
                     UserName = "Rembala",
                     HashedPassword = "XwAQoiq84p1RUzhAyPfaMDKVgSwnn80NCtsE8dNv3XI=",
                     Salt = "mNKLRbEFCH8y1xIyTXP4qA==",
                     IsLocked = false,
                     FirstName = "Artur",
                     LastName = "Maslovskij",
                     DateCreated = DateTime.Now
                 }
             );
            context.Role.Add(new Role() { RoleID = 1, RoleName = "Adminas" });
            context.Role.Add(new Role() { RoleID = 2, RoleName = "Vartotojas" });
            context.Role.Add(new Role() { RoleID = 3, RoleName = "Moderatorius" });

            context.UserRole.Add(new UserRole() { RoleID = 1, UserID = 1 });

            context.ProductType.Add(new ProductType() { ProductTypeID = 1, ProductTypeName = "Kompiuteriai" });
            context.ProductType.Add(new ProductType() { ProductTypeID = 2, ProductTypeName = "Zaislai" });
            context.ProductType.Add(new ProductType() { ProductTypeID = 3, ProductTypeName = "Masinos" });
            context.ProductType.Add(new ProductType() { ProductTypeID = 4, ProductTypeName = "Kita technika" });

            context.Client.Add(new Client() { ClientName = "Daugpils" });
            context.Client.Add(new Client() {  ClientName = "MoscowCity" });
            context.Client.Add(new Client() { ClientName = "VilniuCity" });
            context.Client.Add(new Client() {  ClientName = "Pentagon" });

            context.Status.Add(new Status() { StatusID = 1, StatusName = "Pateikta" });
            context.Status.Add(new Status() { StatusID = 2, StatusName = "Vykdoma" });
            context.Status.Add(new Status() { StatusID = 3, StatusName = "Nutraukta" });

            context.Status.Add(new Status() { StatusID = 4, StatusName = "Užbaikta" });

            context.Country.Add(new Country() { CountryName = "Lietuva" });
            context.Country.Add(new Country() { CountryName = "Rusija" });
            context.Country.Add(new Country() { CountryName = "JAV" });

            context.Product.Add(new Product() { ProductName = "BMW", IsActive = true, ProductTypeID = 3, CreatetionDate = DateTime.Now });
            context.Product.Add(new Product() { ProductName = "Mersedez", IsActive = true, ProductTypeID = 3, CreatetionDate = DateTime.Now });
            context.Product.Add(new Product() { ProductName = "Pegeout", IsActive = true, ProductTypeID = 3, CreatetionDate = DateTime.Now });
            context.Product.Add(new Product() { ProductName = "Iphone", IsActive = true, ProductTypeID = 4, CreatetionDate = DateTime.Now });
            context.Product.Add(new Product() { ProductName = "Samsung", IsActive = true, ProductTypeID = 4, CreatetionDate = DateTime.Now });
            context.Product.Add(new Product() { ProductName = "Acer PC", IsActive = true, ProductTypeID = 1, CreatetionDate = DateTime.Now });
            context.Product.Add(new Product() { ProductName = "Samsung PC", IsActive = true, ProductTypeID = 1, CreatetionDate = DateTime.Now });
            context.Product.Add(new Product() { ProductName = "Lego", IsActive = true, ProductTypeID = 2, CreatetionDate = DateTime.Now });
            for (int i = 0; i < GenerateOrders().Count(); i++)
            {
                context.Order.Add(GenerateOrders()[i]);
            }
            context.Commit();
            context.OrderProduct.Add(new OrderProduct() { ProductID = 1, OrderID = 3, CreationDate = DateTime.Now });
            context.OrderProduct.Add(new OrderProduct() { ProductID = 2, OrderID = 1, CreationDate = DateTime.Now });
            context.OrderProduct.Add(new OrderProduct() { ProductID = 3, OrderID = 1, CreationDate = DateTime.Now });
            context.OrderProduct.Add(new OrderProduct() { ProductID = 4, OrderID = 3, CreationDate = DateTime.Now });
            context.OrderProduct.Add(new OrderProduct() { ProductID = 2, OrderID = 1, CreationDate = DateTime.Now });
            context.OrderProduct.Add(new OrderProduct() { ProductID = 3, OrderID = 2, CreationDate = DateTime.Now });
            context.OrderProduct.Add(new OrderProduct() { ProductID = 1, OrderID = 4, CreationDate = DateTime.Now });
            context.Commit();
        }

        private Order[] GenerateOrders()
        {
            Order[] Order = new Order[]{
                new Order(){
                     ClientID = 1,
                    CountryID = 1,
                     IsActive = true,
                    OrderTime = DateTime.Now,
                     PlannedDate = DateTime.Now.AddDays(12),
                    UserID = 1,
                StatusID = 2,
                 OrderNo = 29
                       
                },
                 new Order(){
                    ClientID = 1,
                    CountryID = 2,
                    OrderTime = DateTime.Now,
                                         IsActive = true,
                                     OrderNo = 22,
                    PlannedDate = DateTime.Now.AddDays(11),
                    UserID = 1,
                StatusID = 2
                },
                 new Order(){
                    ClientID = 3,
                                         IsActive = true,
                    CountryID = 1,
                StatusID = 2,
                                 OrderNo = 25,
                    OrderTime = DateTime.Now,
                    PlannedDate = DateTime.Now.AddDays(10),

                    UserID = 1
                },
                 new Order(){
                    ClientID = 2, 
                    StatusID = 2,
                                     OrderNo = 12,
                    CountryID = 1,
                    OrderTime = DateTime.Now,
                     IsActive = true,
                    PlannedDate = DateTime.Now.AddDays(5),
                    UserID = 1
                    
                },new Order(){
                    ClientID = 2, 
                    StatusID = 2,
                                     OrderNo = 15,
                    CountryID = 1,
                    OrderTime = DateTime.Now,
                     IsActive = true,
                    PlannedDate = DateTime.Now.AddDays(5),
                    UserID = 1                    
                }
                };
            return Order;
        }
    }
}