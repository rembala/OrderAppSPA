using OrdersEntities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orders.Models
{
    public class OrderViewModel : IValidatableObject
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public int OrderNo { get; set; }
        public int UserId { get; set; }
        public int ProductCount { get; set; }
        public string UserName { get; set; }
        public List<Product> Products { get; set; }
        public int StatusId { get; set; }
        public string UserFullName { get; set; }
        public string statusName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime PlannedDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}