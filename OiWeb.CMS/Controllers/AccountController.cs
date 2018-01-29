using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using OiWeb.CMS.Models;
using OiWeb.Entity;
using System.Data;
using System.Data.Entity;
using AttributeRouting.Web.Mvc;
using System.Net;

namespace OiWeb.CMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        [GET("/Login")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;            
            Session.Add("IP", Request.UserHostAddress);
            return View();
        }

        //
        // GET: /Usuarios/{idUser}/Alterar
        [AllowAnonymous]
        [GET("/Usuarios")]
        public ActionResult GetAccounts()
        {   
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Usuários", "fa-table", "Usuários"); 

            var users = Business.CMS_Account.GetUsers();

            return View("/Views/Account/ListUsers.cshtml", users);
        }

        //
        // GET: /Usuarios/{idUser}/Alterar
        [AllowAnonymous]
        [GET("/Usuarios/{idAccount}")]
        public ActionResult GetDetailsAccounts(int idAccount)
        {  
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Usuário", "fa-table", "Usuário");

            var users = Business.CMS_Account.GetUser(idAccount);

            return View("/Views/Account/UserDetailsView.cshtml", users);
        }

        [AllowAnonymous]
        [GET("/Usuarios/Perfil/{idAccount}")]
        public ActionResult GetPerfilAccounts(int idAccount)
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Perfil", "fa-table", "Perfil");

            var users = Business.CMS_Account.GetUser(idAccount);

            return View("/Views/Account/UserPerfilView.cshtml", users);
        }

        [AllowAnonymous]
        [POST("/Account/UpdateUser")]
        public RedirectResult SaveUpdateUser(Entity.CMS_Account user)
        {
            Business.CMS_Account.UpdateUser(user);
            return Redirect("/Usuarios/");
        }

        //
        // GET: /Usuarios/{idUser}/Alterar
        [AllowAnonymous]
        [GET("/Usuarios/Novo/Cadastro")]
        public ActionResult GetNewAccounts()
        {  
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Novo Usuário", "fa-table", "Novo Usuário");
            return View("/Views/Account/UserCreateView.cshtml");
        }
        
        [AllowAnonymous]
        [POST("/Account/CreateUser")]
        public RedirectResult SaveCreateUser(Entity.CMS_Account user)
        {
            Business.CMS_Account.SaveUser(user);
            return Redirect("/Usuarios");
        }

        //
        // POST: /Account/Login       
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginIsValid(LoginViewModel model)
        {            
            if (ModelState.IsValid)
            {
                Entity.CMS_Account account = new Entity.CMS_Account();
                Entity.CMS_AccountLoginLog accountLoginLog = new Entity.CMS_AccountLoginLog();

                account.email = model.Email;
                account.password = model.Password;
                var user = Business.CMS_Account.IsValid(account);
                if (user != null)
                {
                    if (model.returnUrl == null)
                        model.returnUrl = "Dashboard";
                    #region Registra Acesso
                    accountLoginLog.idAccount = user.idAccount;
                    accountLoginLog.IP = Session.Contents["IP"].ToString();
                    accountLoginLog.IsValid = true;
                    accountLoginLog.dtRegister = DateTime.Now;
                    Business.CMS_AccountLoginLog.Create(accountLoginLog);
                    #endregion
                    Session.Add("User", user);
                    Response.Redirect("/Home");
                }
                else
                {
                    #region Registra Acesso Falho
                    account = Business.CMS_Account.IsValidEmail(account);
                    if (account != null)
                    {
                        accountLoginLog.idAccount = account.idAccount;
                        accountLoginLog.IP = Session.Contents["IP"].ToString();
                        accountLoginLog.IsValid = false;
                        accountLoginLog.dtRegister = DateTime.Now;
                        Business.CMS_AccountLoginLog.Create(accountLoginLog);
                    }
                    #endregion
                    ViewBag.Error = "has-error";
                    return View("/Views/Account/Login.cshtml", model);       
                }
            } 
            ViewBag.Error = "has-error";
            return View("/Views/Account/Login.cshtml", model);       
        }        
      
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void SendEmail(string email, string callbackUrl, string subject, string message)
        {
            // For information on sending mail, please visit http://go.microsoft.com/fwlink/?LinkID=320771
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}