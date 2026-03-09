using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagementModels;
using AccountManagementDataService;

namespace AccountManagementAppService
{
    public class AccountAppService
    {
        AccountDataService accountDataService = new AccountDataService();

        public bool Register(Account newAccount)
        {
            if (accountDataService.UsernameExists(newAccount.Username))
                return false;

            var account = new Account
            {
                Username = newAccount.Username,
                Password = newAccount.Password
            };

            accountDataService.Add(account);
            return true;
        }

        public bool ChangePassword(Guid accountID, string newPassword)
        {
            var account = accountDataService.GetById(accountID);

            if (account == null)
                return false;

            account.Password = newPassword;

            accountDataService.Update(account);

            return false;
        }

        public bool Authenticate(string username, string password)
        {
            var account = accountDataService.GetByUsername(username);

            if (account == null)
                return false;

            return account.Password == password;
        }

        public List<Account> GetAccounts()
        {
            return accountDataService.GetAccounts();

        }

        public Account? GetAccount(Guid accountId)
        {
            return accountDataService.GetById(accountId);
        }
    }
}
