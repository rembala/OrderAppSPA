using Orders.Infrastructure.Core;
using Orders.Models;
using OrdersData.Infrastructure;
using OrdersEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        /// <summary>
        /// Gaunamas produktas arba filtruojamas
        /// </summary>
        /// <param name="request"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [Route("search")]
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
        /// <summary>
        /// Gaunamas produktas ir jo tipas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
                                              )
                                              {
                                                  CreationDate = pr.CreatetionDate,
                                                  ProductId = pr.ProductID,
                                                  ProductName = pr.ProductName,
                                                  ProductTypeName = prt.ProductTypeName,
                                              }).ToList();

                     response = this.Request.CreateResponse<IEnumerable<ProductViewModel>>(HttpStatusCode.OK, ProductResult);
                     return response;
                 });
        }
        /// <summary>
        /// Produkto tipo ieskojimas
        /// </summary>
        /// <param name="request"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Naujo produkto pridejimas
        /// </summary>
        /// <param name="request"></param>
        /// <param name="productViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, ProductViewModel productViewModel)
        {
            this._entityTypes = new List<Type>() { typeof(Product) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;
                     this._productRepository.Add(new Product()
                     {
                         CreatetionDate = DateTime.Now,
                         IsActive = true,
                         ProductName = productViewModel.ProductName,
                         ProductTypeID = productViewModel.ProductType.ProductTypeID
                     });
                     this._unitOfWork.Commit();
                     response = this.Request.CreateResponse(HttpStatusCode.Moved);
                     response.Headers.Location = new Uri(@"http://localhost:" + HttpContext.Current.Request.Url.Port + "/api/Product");
                     return response;
                 });
        }
    }
}