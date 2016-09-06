using OrdersEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        public static void OrderExtension(this Order OrderToCreateUpdate, Order Order)
        {
            OrderToCreateUpdate.Client = Order.Client;
            OrderToCreateUpdate.Country = Order.Country;
            OrderToCreateUpdate.IsActive = true;
            OrderToCreateUpdate.Status = Order.Status;
            OrderToCreateUpdate.PlannedDate = Order.PlannedDate;
            OrderToCreateUpdate.OrderNo = Order.OrderNo;
            OrderToCreateUpdate.OrderTime = DateTime.Now;
            OrderToCreateUpdate.User = Order.User;
        }
    }
}