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
    [RoutePrefix("api/Client")]
    public class ClientController : ApiControllerBase
    {
        public ClientController(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork)
            : base(dataRepositoryFactory, unitOfWork)
        {

        }
        /// <summary>
        /// Gaunami kliento duomenys
        /// </summary>
        /// <param name="request"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(HttpRequestMessage request, string filter = null)
        {
            this._entityTypes = new List<Type>() { typeof(Client) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;
                     List<Client> clients = null;
                     if (!string.IsNullOrEmpty(filter))
                         clients = this._clientRepository.FindBy(f => f.ClientName.ToLower().Contains(filter)).Distinct().ToList();
                     else
                         clients = this._clientRepository.GetAll().ToList();
                     response = this.Request.CreateResponse<IEnumerable<Client>>(HttpStatusCode.OK, clients);
                     return response;
                 });
        }
        /// <summary>
        /// Pridedamas naujas klientas
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ClientViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, ClientViewModel ClientViewModel)
        {
            this._entityTypes = new List<Type>() { typeof(Client) };
            return this.CreateHttpResponse(request, this._entityTypes,
                 () =>
                 {
                     HttpResponseMessage response = null;
                     this._clientRepository.Add(new Client()
                     {
                         ClientName = ClientViewModel.ClientName
                     });

                     this._unitOfWork.Commit();
                     response = this.Request.CreateResponse(HttpStatusCode.Moved);
                     response.Headers.Location = new Uri(@"http://localhost:" + HttpContext.Current.Request.Url.Port + "/api/Client");
                     return response;
                 });
        }
    }
}