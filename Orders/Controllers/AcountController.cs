using Orders.Infrastructure.Core;
using OrdersEntities.Entities;
using Orders.Models;
using OrdersData.Infrastructure;
using OrdersService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Orders.Controllers
{
    //[Authorize()]
    [RoutePrefix("api/authentication")]
    public class AcountController : ApiControllerBase
    {
        public AcountController(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork)
            : base(dataRepositoryFactory, unitOfWork) { }

        /// <summary>
        /// Vartotojo registracija
        /// </summary>
        /// <param name="request"></param>
        /// <param name="registerViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public HttpResponseMessage Register(HttpRequestMessage request, RegisterViewModel registerViewModel)
        {
            this._entityTypes = new List<Type>() { typeof(IMembership) };

            return this.CreateHttpResponse(request, this._entityTypes, () =>
            {
                HttpResponseMessage response = null;

                if (!this.ModelState.IsValid)
                {
                    this.Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
                }
                else
                {
                    OrdersEntities.Entities.User _user = this._membershipRepository.
                    CreateUser(registerViewModel.UserName, registerViewModel.Email, registerViewModel.Password, new int[] { 1 });

                    if (_user != null)
                    {
                        response = this.Request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    }
                    else
                    {
                        response = this.Request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                }

                return response;
            });
        }

        [Route("Roles")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            this._entityTypes = new List<Type>() { typeof(Role) };

            return this.CreateHttpResponse(request, this._entityTypes, () =>
                    {
                        HttpResponseMessage respone = null;
                        List<Role> RoleList = null;
                        RoleList = this._roleRepository.GetAll().ToList();
                        respone = request.CreateResponse<IEnumerable<Role>>(HttpStatusCode.OK, RoleList);
                        return respone;
                    });
        }
    }
}