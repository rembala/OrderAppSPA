﻿using Orders.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Orders.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            AutofacWebApiConfig.Initialize(GlobalConfiguration.Configuration);
            //AutoMapperConfiguration.Configure();
        }
    }
}