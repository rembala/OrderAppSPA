using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersData.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        OrdersContext Init();
    }
}
