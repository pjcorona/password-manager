using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Password_Manager_WebApp.Models.Entities;
using Password_Manager_WebApp.Models.ViewModels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Password_Manager_WebApp.Controllers
{
    public class AccessController : Controller
    {
        DatabaseContext context = new DatabaseContext();

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public AccessController() : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DatabaseContext())))
        {
        }

        public AccessController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Username, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, false);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("InvalidLogin", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Access");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        if (error == "Incorrect password.")
                        {
                            ModelState.AddModelError("InvalidCurrentPassword", error);
                        }
                        else if (error == "Passwords must be at least 6 characters.")
                        {
                            ModelState.AddModelError("PasswordMinimumLength", error);
                        }
                    }
                    return View(model);
                }
                TempData["UpdatedAccountDetail"] = "password";
                return RedirectToAction("Index", "Account");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult ChangeUsername()
        {
            return View();
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUsername(ChangeUsernameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(User.Identity.GetUserId());
                user.UserName = model.NewUsername;

                await UserManager.UpdateAsync(user);
                await SignInAsync(user, true);

                TempData["UpdatedAccountDetail"] = "username";
                return RedirectToAction("Index", "Account");
            }
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }


        #region Helpers

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
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
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

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        #endregion
	}
}