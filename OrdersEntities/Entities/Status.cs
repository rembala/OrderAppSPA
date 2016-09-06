using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersEntities.Entities
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public virtual List<Order> Orders { get; set; }
        public Status()
        {
            this.Orders = new List<Order>();
        }
    }
}