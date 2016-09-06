using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersData.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private OrdersContext dbContext;
        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }
        public OrdersContext DbContext
        {
            get { return this.dbContext ?? (this.dbContext = this._dbFactory.Init()); }
        }
        public void Commit()
        {
            this.DbContext.Commit();
        }
    }
}
