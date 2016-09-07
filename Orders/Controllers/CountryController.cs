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
    [RoutePrefix("api/Country")]
    public class CountryController : ApiControllerBase
    {
        public CountryController(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork)
            : base(dataRepositoryFactory, unitOfWork)
        {

        }
        public HttpResponseMessage Get(HttpRequestMessage request, string filter = null)
        {
            this._entityTypes = new List<Type>() { typeof(Country) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;
                     List<Country> Countries = null;
                     if (!string.IsNullOrEmpty(filter))
                         Countries = this._countryRepository.FindBy(f => f.CountryName.ToLower().Contains(filter)).Distinct().ToList();
                     else
                         Countries = this._countryRepository.GetAll().ToList();

                     response = this.Request.CreateResponse<IEnumerable<Country>>(HttpStatusCode.OK, Countries);
                     return response;
                 });
        }
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, CountryViewModel countryViewModel)
        {
            this._entityTypes = new List<Type>() { typeof(Country) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;
                     this._countryRepository.Add(new Country()
                     {
                         CountryName = countryViewModel.CountryName
                     });

                     this._unitOfWork.Commit();
                     response = this.Request.CreateResponse(HttpStatusCode.Moved);
                     response.Headers.Location = new Uri(@"http://localhost:" + HttpContext.Current.Request.Url.Port + "/api/Country");
                     return response;
                 });
        }
    }
}