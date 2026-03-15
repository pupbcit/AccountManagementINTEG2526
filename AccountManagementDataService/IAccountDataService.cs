using AccountManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagementDataService
{
    public interface IAccountDataService
    {
        void Add(Account account);
        Account? GetById(Guid id);
        Account? GetByUsername(string username);
        bool UsernameExists(string username);
        void Update(Account account);
        List<Account> GetAccounts();
    }
}
