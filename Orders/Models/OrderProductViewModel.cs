using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.Models
{
    public class OrderProductViewModel
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreationDate { get; set; }
        public int OrderNo { get; set; }
    }
}