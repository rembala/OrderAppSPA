using OrdersEntities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orders.Models
{
    public class ProductViewModel : IValidatableObject
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public string UserFullName { get; set; }
        public ProductType ProductType { get; set; }
        public DateTime CreationDate { get; set; }
        public int OrderNo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}