using OrdersData;
using Orders.Infrastructure.Extensions;
using OrdersData.Infrastructure;
using OrdersData.Repository;
using OrdersEntities.Entities;
using OrdersService.CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using OrdersService;

namespace Orders.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        protected readonly IDataRepositoryFactory _dataRepositoryFactory;
        protected List<Type> _entityTypes = null;
        protected OrdersContext _OrdersContext;
        #region Enticiai
        protected IEntityBaseRepository<Error> _errorsRepository;
        protected IEntityBaseRepository<Order> _orderRepository;
        protected IEntityBaseRepository<Client> _clientRepository;
        protected IEntityBaseRepository<OrderProduct> _orderProductRepository;
        protected IEntityBaseRepository<Country> _countryRepository;
        protected IEntityBaseRepository<Role> _roleRepository;
        protected IEntityBaseRepository<Product> _productRepository;
        protected IEntityBaseRepository<Status> _statusRepository;
        protected IEntityBaseRepository<User> _userRepository;
        protected IMembership _membershipRepository;
        protected IEntityBaseRepository<ProductType> _productTypeRepository;
        #endregion
        //Kad butu single requestas
        protected IUnitOfWork _unitOfWork;
        protected int UserIdGet
        {
            get
            {
                var userID = (int)System.Web.HttpContext.Current.Session["UserId"];
                return userID;
            }
        }

        private HttpRequestMessage HttpRequestMessage = null;

        public ApiControllerBase(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork)
        {
            this._dataRepositoryFactory = dataRepositoryFactory;
            this._unitOfWork = unitOfWork;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, List<Type> EntityTypes, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                this.HttpRequestMessage = request;
                this.InitRepositories(EntityTypes);
                response = function.Invoke();
            }
            catch (SqlException sqlE)
            {
                if (sqlE.Message.Contains("Could not find stored procedure"))
                {
                    throw new Exception("Reikia executinti proceduru skriptus sql kataloge !");
                }
                LogError(sqlE);
            }
            catch (DbUpdateException ex)
            {
                response = this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return response;
        }

        public void InitRepositories(List<Type> entities)
        {
            if (entities != null)
            {
                if (entities.Any(e => e.GetType().IsAssignableFrom(typeof(MembershipService).GetType())))
                {
                    this._membershipRepository = this.HttpRequestMessage.GetMembershipService();
                }
                if (entities.Any(e => e.FullName == typeof(Role).FullName))
                {
                    this._roleRepository = this._dataRepositoryFactory.GetDataRepository<Role>(this.HttpRequestMessage);
                }
                if (entities.Any(e => e.FullName == typeof(ProductType).FullName))
                {
                    this._productTypeRepository = this._dataRepositoryFactory.GetDataRepository<ProductType>(this.HttpRequestMessage);
                }
                if (entities.Any(e => e.FullName == typeof(Order).FullName))
                {
                    this._orderRepository = this._dataRepositoryFactory.GetDataRepository<Order>(this.HttpRequestMessage);
                }
                if (entities.Any(e => e.FullName == typeof(Client).FullName))
                {
                    this._clientRepository = this._dataRepositoryFactory.GetDataRepository<Client>(this.HttpRequestMessage);
                }
                if (entities.Any(e => e.FullName == typeof(OrderProduct).FullName))
                {
                    this._orderProductRepository = this._dataRepositoryFactory.GetDataRepository<OrderProduct>(this.HttpRequestMessage);
                }
                if (entities.Any(e => e.FullName == typeof(Status).FullName))
                {
                    this._statusRepository = this._dataRepositoryFactory.GetDataRepository<Status>(this.HttpRequestMessage);
                }
                if (entities.Any(e => e.FullName == typeof(Country).FullName))
                {
                    this._countryRepository = this._dataRepositoryFactory.GetDataRepository<Country>(this.HttpRequestMessage);
                }
                if (entities.Any(e => e.FullName == typeof(User).FullName))
                {
                    this._userRepository = this._dataRepositoryFactory.GetDataRepository<User>(this.HttpRequestMessage);
                }
                if (entities.Any(e => e.FullName == typeof(Product).FullName))
                {
                    this._productRepository = this._dataRepositoryFactory.GetDataRepository<Product>(this.HttpRequestMessage);
                }
            }
            this._errorsRepository = this._dataRepositoryFactory.GetDataRepository<Error>(this.HttpRequestMessage);
        }


        private void LogError(Exception ex)
        {
            try
            {
                Error _error = new Error()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.Now
                };
                //_errorRepository.Add(_error);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}