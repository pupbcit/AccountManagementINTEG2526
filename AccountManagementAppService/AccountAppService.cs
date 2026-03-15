using AccountManagementModels;
using AccountManagementDataService;

namespace AccountManagementAppService
{
    public class AccountAppService
    {
        AccountDataService accountDataService = new AccountDataService(new AccountDBData());

        public bool Register(Account newAccount)
        {
            if (accountDataService.UsernameExists(newAccount.Username))
                return false;

            accountDataService.Add(newAccount);
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
