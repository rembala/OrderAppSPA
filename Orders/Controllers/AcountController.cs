using Orders.Infrastructure.Core;
using OrdersEntities.Entities;
using Orders.Models;
using OrdersData.Infrastructure;
using OrdersService.CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrdersService;

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
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public HttpResponseMessage Register(HttpRequestMessage request, RegisterViewModel newUser)
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
                    CreateUser(newUser.UserName, newUser.FirstName, newUser.LastName, newUser.Email, newUser.Password, new int[] { newUser.RoleID });

                    if (_user != null)
                    {
                        response = this.Request.CreateResponse<OrdersEntities.Entities.User>(HttpStatusCode.OK, _user);
                    }
                    else
                    {
                        response = this.Request.CreateResponse<OrdersEntities.Entities.User>(HttpStatusCode.OK, null);
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

        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel userCredentials)
        {
            this._entityTypes = new List<Type>() { typeof(User) };

            return this.CreateHttpResponse(request, _entityTypes, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    MembershipContext _userContext = this._membershipRepository.ValidateUser(userCredentials.UserName, userCredentials.Password);

                    if (_userContext.User != null)
                    {
                        response = this.Request.CreateResponse<OrdersEntities.Entities.User>(HttpStatusCode.OK, _userContext.User);
                    }
                    else
                    {
                        response = this.Request.CreateResponse(HttpStatusCode.NotFound, "Vartotojoas neegzistuoja arba neteisiklingas slaptažodis");
                    }
                }
                else
                    response = this.Request.CreateResponse(HttpStatusCode.OK, new { success = false });

                return response;
            });
        }
    }
}