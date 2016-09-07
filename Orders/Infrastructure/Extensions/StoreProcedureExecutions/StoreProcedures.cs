using Orders.Models;
using Orders.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Orders.Infrastructure.Extensions.StoreProcedureExecutions
{
    public class StoreProcedures : DbContext
    {
        public StoreProcedures()
            : base("OrderCon") { }

        public List<OrderViewModel> OrderProductGet(string Filter)
        {
            return (Database.SqlQuery<OrderViewModel>("EXEC [web].[OrderProduct_Get] @Filter",
                new SqlParameter("@Filter", string.IsNullOrEmpty(Filter) ? (object)DBNull.Value : Filter))).ToList();
            //Reikia susikurti procedura pries executinant sita.
        }
        /// <summary>
        /// Produkto pagal uzsakymo id gavimas
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public List<OrderProductViewModel> ProductOrderGet(int? OrderId)
        {
            return (Database.SqlQuery<OrderProductViewModel>("EXEC [web].[ProductOrder_Get] @OrderId",
            new SqlParameter("@OrderId", !OrderId.HasValue ? (object)DBNull.Value : OrderId))).ToList();
        }
        /// <summary>
        /// Uzsakymo saugojimas
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="OrderTime"></param>
        /// <param name="UserId"></param>
        /// <param name="OrderNo"></param>
        /// <param name="StatusId"></param>
        /// <param name="CountryId"></param>
        /// <param name="ClientId"></param>
        /// <param name="PlannedDate"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public decimal Order_Save(int? OrderId, DateTime OrderTime, int UserId, int OrderNo, int StatusId, int CountryId, int ClientId, DateTime PlannedDate, bool IsActive)
        {
            return (Database.SqlQuery<decimal>("EXEC [web].[Order_Save] @OrderId,@OrderTime,@UserID,@OrderNo,@StatusID,@CountryID,@ClientID,@PlannedDate,@IsActive",
                        new SqlParameter("@OrderId", (object)DBNull.Value),
                        new SqlParameter("@OrderTime", DateTime.Now),
                        new SqlParameter("@UserID", UserId),
                        new SqlParameter("@OrderNo", OrderNo),
                        new SqlParameter("@StatusID", StatusId),
                        new SqlParameter("@CountryID", CountryId),
                        new SqlParameter("@ClientID", ClientId),
                        new SqlParameter("@PlannedDate", PlannedDate),
                        new SqlParameter("@IsActive", IsActive)
                        )).ToList()[0];
        }
        public class MyClass
        {
            public int OrderId { get; set; }
        }
    }
}