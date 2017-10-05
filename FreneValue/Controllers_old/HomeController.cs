using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreneValue.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Cuisine()
        {
            string name = "AAAA 54545454****";
            ViewBag.Message =  "Your contact page .  "+ name;
            //name = "ceci est un test du nom";
            //return RedirectPermanent("https://docs.microsoft.com/fr-fr/dotnet/azure/");

          //  return Content(name);

            return View();
        }
    }
}