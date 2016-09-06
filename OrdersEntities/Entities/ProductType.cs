using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  
namespace OrdersEntities.Entities
{
    public class ProductType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public List<Product> Products { get; set; }
        public ProductType()
        {
            this.Products = new List<Product>();
        }
    }
}
