﻿using OrdersEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public User User { get; set; }

        public bool IsValid()
        {
            return this.Principal != null;
        }
    }
}