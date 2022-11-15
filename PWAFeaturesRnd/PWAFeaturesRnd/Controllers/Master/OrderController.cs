using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PWAFeaturesRnd.Controllers.Master
{
    public class OrderController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }

        public IActionResult ViewQuote()
        {
            return View();
        }
    }
}