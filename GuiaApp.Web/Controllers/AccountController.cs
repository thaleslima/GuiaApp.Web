using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using GuiaApp.Web.Models;
using GuiaApp.Model;
using System.Web.Security;

namespace GuiaApp.Web.Controllers
{

    public class AccountController : Controller
    {
        private BDContext db = new BDContext();

        //
        // GET: /Account/Login
        public ActionResult Login(string returnUrl = "")
        {
            if (Request.IsAuthenticated)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    returnUrl = Server.UrlDecode(returnUrl);
                    return RedirectToAction(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        //
        // GET: /Account/Demo
        public ActionResult Demo()
        {
             FormsAuthentication.SetAuthCookie("Demo", false);
             Session["user"] = "Demo";
             return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(string email, string senha, string returnUrl)
        {
            bool exit = false;

            if (!Request.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("", "Informe um login.");
                    exit = true;
                }
                if (string.IsNullOrEmpty(senha))
                {
                    ModelState.AddModelError("", "Informe uma senha");
                    exit = true;
                }

                if (exit) return View();

                var usuario = db.User.Where(p => p.Email == email && p.Password == senha).ToList();
                if (usuario.Count == 0)
                {
                    ModelState.AddModelError("", "Usuário não cadastrado");
                    return View();
                }

                FormsAuthentication.SetAuthCookie(usuario[0].Name, false);
                Session["user"] = usuario[0];
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                returnUrl = Server.UrlDecode(returnUrl);
                return RedirectToAction(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }


        public ActionResult LogOff()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}