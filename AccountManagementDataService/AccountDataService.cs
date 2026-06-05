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
        IAccountDataService _dataService;
        public AccountDataService(IAccountDataService accountDataService)
        {
            _dataService = accountDataService;
        }

        public void Add(Account account)
        {
            _dataService.Add(account);
        }

        public Account? GetById(Guid id)
        {
            return _dataService.GetById(id);
        }

        public Account? GetByUsername(string username)
        {
            return _dataService.GetByUsername(username);
        }

        public bool UsernameExists(string username)
        {
            return _dataService.UsernameExists(username);
        }

        public void Update(Account account)
        {
            _dataService.Update(account);
        }

        public List<Account> GetAccounts()
        {
            return _dataService.GetAccounts();
        }

        public void Delete(Guid id)
        {
             _dataService.Delete(id);
        }

    }
}
