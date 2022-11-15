using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PWAFeaturesRnd.Controllers.Base;

namespace PWAFeaturesRnd.Controllers.Master
{
    public class PMSController : AuthenticatedController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}