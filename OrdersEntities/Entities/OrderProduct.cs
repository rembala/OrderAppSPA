using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersEntities.Entities
{
    public class OrderProduct
    {
        [Key]
        public int OrderProductId { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Order Order { get; set; }
        public int OrderID { get; set; }
        public virtual Product Product { get; set; }
        public int ProductID { get; set; }
    }
}
