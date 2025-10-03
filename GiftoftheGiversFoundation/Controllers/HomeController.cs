using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GiftoftheGiversFoundation.Models;

namespace GiftoftheGiversFoundation.Controllers
{
    // Controls the main public-facing pages like the Homepage and Privacy Policy.
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
