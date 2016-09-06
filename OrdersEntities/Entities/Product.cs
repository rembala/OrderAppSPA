using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersEntities.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatetionDate { get; set; }
        public bool IsActive { get; set; }
        public int ProductTypeID { get; set; }
        public virtual ProductType ProductType { get; set; }

    }
}