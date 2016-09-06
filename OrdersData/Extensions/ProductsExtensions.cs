using OrdersData.Repository;
using OrdersEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersData.Extensions
{
    public static class ProductsExtensions
    {
        public static IEnumerable<OrderProduct> GetAvailableProductsItems(this IEntityBaseRepository<OrderProduct> OrderProducts, int orderId)
        {
            return OrderProducts.GetAll().Where(f => f.OrderID == orderId).ToList();
        }
    }
}