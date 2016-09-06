using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersData.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        OrdersContext dbContext;
        public OrdersContext Init()
        {
            return this.dbContext ?? (dbContext = new OrdersContext());
        }
    }
}
