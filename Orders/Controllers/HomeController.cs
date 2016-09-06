using OrdersData;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orders.Infrastructure.Core;
using AutoMapper;
using OrdersEntities.Entities;
using Orders.Models;

namespace Orders.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}