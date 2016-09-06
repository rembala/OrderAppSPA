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
    [RoutePrefix("api/Country")]
    public class CountryController : ApiControllerBase
    {
        public CountryController(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork)
            : base(dataRepositoryFactory, unitOfWork)
        {

        }
        public HttpResponseMessage Get(HttpRequestMessage request, string filter)
        {
            this._entityTypes = new List<Type>() { typeof(Country) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;

                     var Country = this._countryRepository.FindBy(f => f.CountryName.ToLower().Contains(filter)).Distinct().ToList();
                     response = this.Request.CreateResponse<IEnumerable<Country>>(HttpStatusCode.OK, Country);
                     return response;
                 });
        }
    }
}
