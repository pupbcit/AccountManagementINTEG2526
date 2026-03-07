using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagementModels;

namespace AccountManagementDataService
{
    public class AccountDataService
    {
        public List<Account> dummyAccounts = new List<Account>();

        public AccountDataService()
        {
            Account adminAccount = new Account { Username = "Admin", Password = "Admin123!" };

            Account userAccount = new Account { Username = "user", Password = "Password123!" };

            Account guestAccount = new Account { Username = "guest", Password = "Guest123!" };

            dummyAccounts.Add(adminAccount);
            dummyAccounts.Add(userAccount);
            dummyAccounts.Add(guestAccount);
        }

        public bool IsUsernameExists(string username)
        {
            var result = dummyAccounts.Where(x => x.Username == username);

            return result.Any();
        }

        public void AddAccount(Account account)
        {
            dummyAccounts.Add(account);
        }
    }
}
