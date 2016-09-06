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
    [RoutePrefix("api/Client")]
    public class ClientController : ApiControllerBase
    {
        public ClientController(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork)
            : base(dataRepositoryFactory, unitOfWork)
        {

        }

        public HttpResponseMessage Get(HttpRequestMessage request, string filter)
        {
            this._entityTypes = new List<Type>() { typeof(Client) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;

                     var Client = this._clientRepository.FindBy(f => f.ClientName.ToLower().Contains(filter)).Distinct().ToList();
                     response = this.Request.CreateResponse<IEnumerable<Client>>(HttpStatusCode.OK, Client);
                     return response;
                 });
        }
    }
}