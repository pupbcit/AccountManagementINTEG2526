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
            Account adminAccount = new Account { AccountId = Guid.NewGuid(), Username = "admin", Password = "admin123!" };

            Account userAccount = new Account { AccountId = Guid.NewGuid(), Username = "user", Password = "user123!" };

            Account guestAccount = new Account { AccountId = Guid.NewGuid(), Username = "guest", Password = "guest123!" };

            dummyAccounts.Add(adminAccount);
            dummyAccounts.Add(userAccount);
            dummyAccounts.Add(guestAccount);
        }

        public void Add(Account account)
        {
            dummyAccounts.Add(account);
        }

        public Account? GetById(Guid id)
        {
            return dummyAccounts.FirstOrDefault(a => a.AccountId == id);
        }

        public Account? GetByUsername(string username)
        {
            return dummyAccounts.FirstOrDefault(a => a.Username == username);
        }

        public bool UsernameExists(string username)
        {
            return dummyAccounts.Any(a => a.Username == username);
        }

        public void Update(Account account)
        {
            var existing = GetById(account.AccountId);
            if (existing != null)
            {
                existing.Username = account.Username;
                existing.Password = account.Password;
            }
        }

        public List<Account> GetAccounts()
        {
            return dummyAccounts;
        }
    }
}
