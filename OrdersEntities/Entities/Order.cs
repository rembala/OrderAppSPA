using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersEntities.Entities
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderTime { get; set; }
        public virtual User User { get; set; }
        public int UserID { get; set; }
        public virtual Status Status { get; set; }
        public int OrderNo { get; set; }
        public int? StatusID { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; }
        public virtual Country Country { get; set; }
        public int CountryID { get; set; }
        public virtual Client Client { get; set; }
        public int ClientID { get; set; }
        public DateTime PlannedDate { get; set; }
        public bool IsActive { get; set; }
        public Order()
        {
            this.OrderProducts = new List<OrderProduct>();
        }
    }
}