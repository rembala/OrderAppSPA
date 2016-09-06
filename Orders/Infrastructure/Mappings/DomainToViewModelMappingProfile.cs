using AutoMapper;
using Orders.Models;
using OrdersEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.Infrastructure.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "OrderMappings";
            }
        }

        [Obsolete]
        protected override void Configure()
        {
            this.CreateMap<Order, OrderViewModel>()
                .ForMember(g => g.OrderId, map => map.MapFrom(h => h.OrderID));
            this.CreateMap<Client, OrderViewModel>()
                .ForMember(b => b.ClientId, map => map.MapFrom(g => g.ClientID))
                .ForMember(g => g.ClientName, map => map.MapFrom(h => h.ClientName));
        }
    }
}