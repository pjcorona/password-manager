using Password_Manager_WebApp.Helpers;
using Password_Manager_WebApp.Models.Entities;
using Password_Manager_WebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Password_Manager_WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        DatabaseContext context = new DatabaseContext();
        PasswordHelper passwordHelper = new PasswordHelper();

        public ActionResult Index(string searchKeyword, bool showInactive = false)
        {
            ViewBag.SearchKeyword = searchKeyword;
            ViewBag.ShowInactive = showInactive;

            List<Account> accounts = new List<Account>();

            if (!String.IsNullOrEmpty(searchKeyword) && showInactive != true)
            {
                accounts = context.Accounts.Where(a => a.Status == "Active" && (a.ApplicationName.Contains(searchKeyword) ||
                    a.Username.Contains(searchKeyword) ||
                    a.EmailAddress.Contains(searchKeyword))).ToList();
            }
            else if (!String.IsNullOrEmpty(searchKeyword) && showInactive == true)
            {
                accounts = context.Accounts.Where(a => a.ApplicationName.Contains(searchKeyword) ||
                    a.Username.Contains(searchKeyword) ||
                    a.EmailAddress.Contains(searchKeyword)).ToList();
            }
            else if (String.IsNullOrEmpty(searchKeyword) && showInactive != true)
            {
                accounts = context.Accounts.Where(a => a.Status == "Active").ToList();
            }
            else if (String.IsNullOrEmpty(searchKeyword) && showInactive == true)
            {
                accounts = context.Accounts.ToList();
            }

            return View(accounts);
        }

        public ActionResult NewAccount()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult NewAccount(AccountEntryViewModel accountEntry)
        {
            if (ModelState.IsValid)
            {
                Account validatedAccount = accountEntry.AccountDetails;
                validatedAccount.Password = passwordHelper.EncryptPassword(accountEntry.Password);
                validatedAccount.Status = "Active";
                
                context.Accounts.Add(validatedAccount);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountEntry);
        }
       
        public ActionResult ViewDetails(int id)
        {
            Account account = context.Accounts.Find(id);
            account.Password = passwordHelper.DecryptPassword(account.Password);
            return PartialView(account);
        }

        public ActionResult EditDetails(int id)
        {
            Account account = context.Accounts.Find(id);
            AccountEntryViewModel accountEntry = new AccountEntryViewModel();
            accountEntry.AccountDetails = account;
            EditAccountViewModel editAccountVM = new EditAccountViewModel();
            editAccountVM.AccountEntry = accountEntry;
            return View(editAccountVM);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditDetails(EditAccountViewModel editAccountVM)
        {
            string originalPassword;
            using (DatabaseContext own_context = new DatabaseContext())
            {
                Account originalAccount = own_context.Accounts.Find(editAccountVM.AccountEntry.AccountDetails.AccountID);
                originalPassword = originalAccount.Password;
            }            

            Account modifiedAccount = editAccountVM.AccountEntry.AccountDetails;
            if (editAccountVM.ChangePasswordFlag)
            {
                if (!string.IsNullOrWhiteSpace(editAccountVM.AccountEntry.Password) && !string.IsNullOrWhiteSpace(editAccountVM.AccountEntry.RepeatPassword))
                {
                    modifiedAccount.Password = passwordHelper.EncryptPassword(editAccountVM.AccountEntry.Password);
                }
            }
            else
            {
                modifiedAccount.Password = originalPassword;
            }

            if (ModelState.IsValid)
            {
                context.Entry(modifiedAccount).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editAccountVM);
        }
	}
}