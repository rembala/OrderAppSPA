using Orders.Infrastructure.Core;
using Orders.Models;
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
        [Route("filter")]
        public HttpResponseMessage Get(HttpRequestMessage request, string filter = null)
        {
            this._entityTypes = new List<Type>() { typeof(Product) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;
                     List<Product> ProductList = null;
                     ProductList = this._productRepository.FindBy(f => f.ProductName.ToLower().Contains(filter)).Distinct().ToList();

                     response = this.Request.CreateResponse<IEnumerable<Product>>(HttpStatusCode.OK, ProductList);
                     return response;
                 });
        }
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            this._entityTypes = new List<Type>() { typeof(Product), typeof(ProductType) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;
                     var ProductResult = (from pr in this._productRepository.GetAll()
                                          join prt in this._productTypeRepository.GetAll() on pr.ProductTypeID equals prt.ProductTypeID
                                          select new ProductViewModel
                                              (
                                              pr.ProductID,
                                              pr.ProductName,
                                              prt.ProductTypeName,
                                              pr.CreatetionDate
                                              )).ToList();

                     response = this.Request.CreateResponse<IEnumerable<ProductViewModel>>(HttpStatusCode.OK, ProductResult);
                     return response;
                 });
        }

        [HttpGet]
        [Route("ProductTypeChoose")]
        public HttpResponseMessage ProductTypeFilter(HttpRequestMessage request, string filter = null)
        {
            this._entityTypes = new List<Type>() { typeof(ProductType) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;
                     List<ProductType> ProductTypeList = null;
                     if (!string.IsNullOrEmpty(filter))
                         ProductTypeList = this._productTypeRepository.FindBy(f => f.ProductTypeName.ToLower().Contains(filter)).Distinct().ToList();
                     else
                         ProductTypeList = this._productTypeRepository.GetAll().ToList();
                     response = this.Request.CreateResponse<IEnumerable<ProductType>>(HttpStatusCode.OK, ProductTypeList);
                     return response;
                 });
        }
    }
}