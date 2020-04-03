using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.User;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Profile(UserProfileForm userForm)
        {
            ViewData["UserId"] = userForm.UserId;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ProfileComplete(UserProfileForm userForm)
        {
            ViewData["SuccessMessage"] = string.Format(GlobalResources.Resources.ProfileComplete_Success_Message, userForm.FullName);
            return View();
        }


    }
}