using Microsoft.AspNetCore.Mvc;

namespace PWAFeaturesRnd.Controllers.Master
{
	public class VesselController : Controller
	{
		public IActionResult PositionList()
		{
			return View();
		}

		public IActionResult VesselPositionList()
		{
			return View();
		}

		public IActionResult PortCallLocation()
		{
			return View();
		}
		public IActionResult SeaPassage()
		{
			return View();
		}
	}
}
