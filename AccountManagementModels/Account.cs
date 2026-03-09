using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagementModels
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
