using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.Infrastructure.Extensions
{
    public static class ChefForNullValues
    {
        public static string ToSafeString(this object objc)
        {
            return (objc ?? string.Empty).ToString();
        }

        public static object CheckForNullValues(this object CurrentObject)
        {
            return string.IsNullOrEmpty(CurrentObject.ToSafeString()) ? (object)DBNull.Value : CurrentObject;
        }
    }
}