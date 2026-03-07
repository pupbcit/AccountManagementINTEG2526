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
        //behavior lang...
        public void Register(Account account)
        {
            AccountDataService accountDataService = new AccountDataService();

            if (!accountDataService.IsUsernameExists(account.Username))
            {
                accountDataService.AddAccount(account);
            }
        }
    }
}
