using Orders.Infrastructure.Core;
using Orders.Infrastructure.Extensions.StoreProcedureExecutions;
using Orders.Models;
using Orders.Infrastructure.Core;
using OrdersData.Infrastructure;
using Orders.Infrastructure.Extensions;
using OrdersData.Repository;
using OrdersEntities.Entities;
using OrdersData.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrdersData;

namespace Orders.Controllers
{
    //[Authorize(Roles = "Admin")]
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiControllerBase
    {
        //private readonly IUnitOfWork unitOfWork;
        public OrdersController(IDataRepositoryFactory dataRepositoryFactory, IUnitOfWork unitOfWork, IEntityBaseRepository<Order> Order)
            : base(dataRepositoryFactory, unitOfWork)
        {

        }

        /// <summary>
        /// Visi uzsakymai praeito ir sio menesio
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            this._entityTypes = new List<Type>() { typeof(Order), typeof(Status), typeof(Client), typeof(User), typeof(Country), typeof(Order) };

            return this.CreateHttpResponse(request, this._entityTypes,
                () =>
                {
                    HttpResponseMessage response = null;
                    var OrderViewModelList = new ProcedureExecution().OrderProductGet(null);

                    #region Kadangi daroma su code first tai su linq, bet optimaliau manau butu per procedura
                    //var OrderViewModelList = (from ord in Orders
                    //                          join st in this._statusRepository.GetAll() on ord.StatusID equals st.StatusID
                    //                          join us in this._userRepository.GetAll() on ord.UserID equals us.UserID
                    //                          join ct in this._countryRepository.GetAll() on ord.CountryID equals ct.CountryID
                    //                          join cl in this._clientRepository.GetAll() on ord.ClientID equals cl.ClientID
                    //                          where ord.OrderTime >= DateTime.Now.AddMonths(-1) && ord.OrderTime <= DateTime.Now
                    //                          select new OrderViewModel()
                    //                          {
                    //                              ClientId = cl.ClientID,
                    //                              ClientName = cl.ClientName,
                    //                              CountryId = ct.CountryID,
                    //                              CountryName = ct.CountryName,
                    //                              OrderId = ord.OrderID,
                    //                              OrderTime = ord.OrderTime,
                    //                              PlannedDate = ord.PlannedDate,
                    //                              StatusId = st.StatusID,
                    //                              statusName = st.StatusName,
                    //                              UserId = us.UserID,
                    //                              UserName = us.UserName,
                    //                              UserFullName = us.FirstName + ' ' + us.LastName,
                    //                              OrderNo = ord.OrderNo,
                    //                              ProductCount = ord.Products.Count()
                    //                          }).ToList();
                    #endregion

                    response = this.Request.CreateResponse<IEnumerable<OrderViewModel>>(HttpStatusCode.OK, OrderViewModelList);
                    return response;
                }
            );
        }
        /// <summary>
        /// Gaunami produktai pagal Order Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [Route("{OrderId:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int OrderId)
        {
            this._entityTypes = new List<Type>() { typeof(OrderProduct) };
            return this.CreateHttpResponse(request, this._entityTypes,
                () =>
                {
                    HttpResponseMessage ResponseMessage = null;

                    var OrderViewModelList = new ProcedureExecution().ProductOrderGet(OrderId);

                    ResponseMessage = this.Request.CreateResponse<IEnumerable<OrderProductViewModel>>(HttpStatusCode.OK, OrderViewModelList);
                    return ResponseMessage;
                }
              );
        }
        [Route("{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string Filter = null)
        {
            this._entityTypes = new List<Type>() { typeof(Order) };
            return this.CreateHttpResponse(request, _entityTypes,
                () =>
                {
                    var a = this._orderRepository.GetAll().ToList();
                    int CurrentPage = page.HasValue ? (int)page : 0;
                    int PageSize = pageSize.HasValue ? (int)pageSize : 0;
                    HttpResponseMessage ResponseMessage = null;
                    List<OrderViewModel> OrderItems = null;
                    var TotalOrderItems = new ProcedureExecution().OrderProductGet(Filter);

                    OrderItems = TotalOrderItems.Skip(CurrentPage * PageSize)
                                                    .Take(PageSize).ToList();

                    PaginationSet<OrderViewModel> OrderPageSet = new PaginationSet<OrderViewModel>()
                    {
                        Page = CurrentPage,
                        Items = OrderItems,
                        TotalItemsCount = TotalOrderItems.Count(),
                        TotalPages = (int)Math.Ceiling((decimal)TotalOrderItems.Count() / PageSize)
                    };

                    ResponseMessage = this.Request.CreateResponse<PaginationSet<OrderViewModel>>(HttpStatusCode.OK, OrderPageSet);
                    return ResponseMessage;

                }
  );
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, OrderViewModel order)
        {
            this._entityTypes = new List<Type>() { typeof(OrderProduct) };

            return this.CreateHttpResponse(request, this._entityTypes,
                   () =>
                   {
                       HttpResponseMessage response = null;
                       int OrderId = Convert.ToInt32(new ProcedureExecution().Order_Save(null, DateTime.Now, 1, order.OrderNo, 2, order.CountryId, order.ClientId, order.PlannedDate, true));
                       foreach (Product item in order.Products)
                       {
                           this._orderProductRepository.Add(new OrderProduct()
                           {
                               CreationDate = DateTime.Now,
                               OrderID = OrderId,
                               ProductID = item.ProductID
                           });
                       }
                       this._unitOfWork.Commit();
                       var OrderViewModelList = new ProcedureExecution().OrderProductGet(null);
                       return response = this.Request.CreateResponse<IEnumerable<OrderViewModel>>(HttpStatusCode.OK, OrderViewModelList);
                   });
        }
    }
}