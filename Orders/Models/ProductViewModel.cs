using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orders.Models
{
    public class ProductViewModel : IValidatableObject
    {
        public ProductViewModel(int productId, string productName, string productTypeName, DateTime creationDate)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.ProductTypeName = productTypeName;
            this.CreationDate = creationDate;
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreationDate { get; set; }
        public int OrderNo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}