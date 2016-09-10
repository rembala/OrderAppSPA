using OrdersEntities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersData
{
    public class OrdersContext : DbContext
    {
        public OrdersContext()
            : base("OrderCon")
        {
            //TODO: Sita atkomentuokite, jeigu nenori kad pasikratu DB su inicializutais duomenimis, o tada sekancia eiltutę užkomentuokite.
            //Database.Initialize(true);
            Database.SetInitializer(new OrderContextInitializer());
        }
        public IDbSet<Order> Order { get; set; }
        public IDbSet<User> User { get; set; }
        public IDbSet<OrderProduct> OrderProduct { get; set; }
        public IDbSet<UserRole> UserRole { get; set; }
        public IDbSet<Role> Role { get; set; }
        public IDbSet<ProductType> ProductType { get; set; }
        public IDbSet<Product> Product { get; set; }
        public IDbSet<Country> Country { get; set; }
        public IDbSet<Client> Client { get; set; }
        public IDbSet<Status> Status { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region DB inicializavimas
            //Cascade on delete is enabled by default
            modelBuilder.Entity<Order>().ToTable("Order", "web");
            modelBuilder.Entity<User>().ToTable("User", "authentication");
            modelBuilder.Entity<OrderProduct>().ToTable("OrderProduct", "web");
            modelBuilder.Entity<User>().Property(f => f.UserName).IsUnicode(false).HasMaxLength(30);
            modelBuilder.Entity<User>().Property(f => f.Email).IsUnicode(false).HasMaxLength(100);
            modelBuilder.Entity<UserRole>().ToTable("UserRole", "authentication");
            modelBuilder.Entity<Role>().ToTable("Role", "authentication")
                .Property(g => g.RoleName).IsUnicode(false).HasMaxLength(50);
            modelBuilder.Entity<ProductType>().ToTable("ProductType", "web")
                .Property(g => g.ProductTypeName).IsUnicode(false).HasMaxLength(50);
            modelBuilder.Entity<Product>().ToTable("Product", "web")
                .Property(g => g.ProductName).IsUnicode(false).HasMaxLength(50);
            modelBuilder.Entity<Country>().ToTable("Country", "web")
                .Property(g => g.CountryName).IsUnicode(false).HasMaxLength(50);
            modelBuilder.Entity<Client>().ToTable("Client", "web")
                .Property(g => g.ClientName).IsUnicode(false).HasMaxLength(50);
            modelBuilder.Entity<Status>().ToTable("Status", "web")
                .Property(g => g.StatusName).IsUnicode(false).HasMaxLength(50);
            #endregion
        }
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        //public void ExecuteStoreProcuderues<T>(string ProcedureName) where T : class
        //{
        //    this.Database.SqlQuery<T>("EXEC" + ProcedureName + "");
        //}
    }
}
