namespace Password_Manager_WebApp.Migrations
{
    using Password_Manager_WebApp.Helpers;
    using Password_Manager_WebApp.Models.Entities;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        PasswordHelper pwHelper = new PasswordHelper();

        protected override void Seed(DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            var identityManager = new IdentityHelper();
            var user = new ApplicationUser()
            {
                UserName = "username1"
            };
            identityManager.CreateUser(user, "password1");

            new List<Account>
            {
                new Account { AccountID = 1, Status = "Active", ApplicationName = "Facebook", EmailAddress = "pjoe_boston2010@yahoo.com", Username = "pjcorona", MobileNumber = "09154606581", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 2, Status = "Active", ApplicationName = "Twitter", EmailAddress = "corona.princejomer@gmail.com", Username = "kuriboh", MobileNumber = "09154606581", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 3, Status = "Active", ApplicationName = "Instagram", EmailAddress = "pjoe_boston2010@yahoo.com", Username = "kuriboh", MobileNumber = "09306326211", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 4, Status = "Active", ApplicationName = "Twitch", EmailAddress = "princejomer@yahoo.com", Username = "pjcorona", MobileNumber = "09114567887", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 5, Status = "Active", ApplicationName = "Steam", EmailAddress = "pjoe_boston2010@yahoo.com", Username = "kuriboh", MobileNumber = "09306326211", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 6, Status = "Active", ApplicationName = "Garena", EmailAddress = "princejomer@gmail.com", Username = "pjcorona", MobileNumber = "09154606581", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 7, Status = "Active", ApplicationName = "Quora", EmailAddress = "princejomer@yahoo.com", Username = "pulpy", MobileNumber = "09154606581", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 8, Status = "Active", ApplicationName = "Grab", EmailAddress = "pjoe_boston2010@gmail.com", Username = "pjcorona", MobileNumber = "09306326211", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 9, Status = "Active", ApplicationName = "Gmail", EmailAddress = "princejomer@yahoo.com", Username = "pulpy", MobileNumber = "09154606581", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 10, Status = "Active", ApplicationName = "Chinabank", EmailAddress = "princejomer@gmail.com", Username = "pulpy", MobileNumber = "09154606581", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 11, Status = "Active", ApplicationName = "Facebook", EmailAddress = "pjoe_boston2010@yahoo.com", Username = "pjcorona", MobileNumber = "09306326211", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 12, Status = "Active", ApplicationName = "Yahoo", EmailAddress = "christiansantos345@yahoo.com", Username = "christiansantos345", MobileNumber = "09114567887", Password = pwHelper.EncryptPassword("qwerty@1")},
                new Account { AccountID = 13, Status = "Active", ApplicationName = "Instagram", EmailAddress = "christiansantos345@yahoo.com", Username = "christiansantos345", MobileNumber = "09154606581", Password = pwHelper.EncryptPassword("qwerty@1")}
            }.ForEach(a => context.Accounts.AddOrUpdate(a));
        }
    }
}
