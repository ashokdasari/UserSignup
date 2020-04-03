using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Home;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Signup()
        {
            var model = new SignupForm();

            return View("Signup", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            var model = new SignupForm();

            return View("Signin", model);
        }


    }
}