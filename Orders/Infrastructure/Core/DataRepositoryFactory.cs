using OrdersData.Repository;
using System;
using System.Collections.Generic;
using Orders.Infrastructure.Extensions;
using System.Linq;
using System.Net.Http;
using System.Web;
using OrdersService.Abstract;

namespace Orders.Infrastructure.Core
{
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        public IEntityBaseRepository<T> GetDataRepository<T>(HttpRequestMessage request) where T : class,new()
        {
            return request.GetDataRepository<T>();
        }
    }

    public interface IDataRepositoryFactory
    {
        IEntityBaseRepository<T> GetDataRepository<T>(HttpRequestMessage request) where T : class, new();
    }
}