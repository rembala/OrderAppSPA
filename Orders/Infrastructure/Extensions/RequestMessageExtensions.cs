﻿using OrdersData.Repository;
using OrdersService.CustomAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Orders.Infrastructure.Extensions
{
    public static class RequestMessageExtensions
    {
        public static IMembership GetMembershipService(this HttpRequestMessage request)
        {
            return request.GetService<IMembership>();
        }

        internal static IEntityBaseRepository<T> GetDataRepository<T>(this HttpRequestMessage request) where T : class, new()
        {
            return request.GetService<IEntityBaseRepository<T>>();
        }

        private static TService GetService<TService>(this HttpRequestMessage request)
        {
            //Sukuria new entity object
            return (TService)request.GetDependencyScope().GetService(typeof(TService));
        }
    }
}