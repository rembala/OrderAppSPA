using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersEntities.Entities
{
    public class Client
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public virtual List<Order> Orders { get; set; }
        public bool IsDeleted { get; set; }
        public Client()
        {
            this.Orders = new List<Order>();
        }
    }
}
