using Orders.Infrastructure.Core;
using OrdersData.Infrastructure;
using OrdersEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Orders.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiControllerBase
    {
        public ProductController(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork)
            : base(dataRepositoryFactory, unitOfWork)
        {

        }
        public HttpResponseMessage Get(HttpRequestMessage request, string filter)
        {
            this._entityTypes = new List<Type>() { typeof(Product) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;

                     var Product = this._productRepository .FindBy(f => f.ProductName.ToLower().Contains(filter)).Distinct().ToList();
                     response = this.Request.CreateResponse<IEnumerable<Product>>(HttpStatusCode.OK, Product);
                     return response;
                 });
        }
    }
}