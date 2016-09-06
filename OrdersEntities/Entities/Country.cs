using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersEntities.Entities
{
    public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public virtual List<Order> Orders { get; set; }
        public bool IsDeleted { get; set; }
        public Country()
        {
            this.Orders = new List<Order>();
        }
    }
}