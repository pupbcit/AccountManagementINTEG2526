using AccountManagementModels;
using System.Text.Json;

namespace AccountManagementDataService
{
    public class AccountJsonData : IAccountDataService
    {
        private List<Account> accounts = new List<Account>();

        private string _jsonFileName;

        public AccountJsonData()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/Accounts.json";

            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile();

            if (accounts.Count <= 0)
            {
                accounts.Add(new Account { AccountId = Guid.NewGuid(), Username = "admin", Password = "admin123!" });
                accounts.Add(new Account { AccountId = Guid.NewGuid(), Username = "user", Password = "user123!" });
                accounts.Add(new Account { AccountId = Guid.NewGuid(), Username = "guest", Password = "guest123!" });
                accounts.Add(new Account { AccountId = Guid.NewGuid(), Username = "indaleen", Password = "indaleen123!" });

                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize<List<Account>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , accounts);
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            using (var jsonFileReader = File.OpenText(this._jsonFileName))
            {
                this.accounts = JsonSerializer.Deserialize<List<Account>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }

        public void Add(Account account)
        {
            accounts.Add(account);
            SaveDataToJsonFile();
        }

        public List<Account> GetAccounts()
        {
            RetrieveDataFromJsonFile();
            return accounts;
        }

        public Account? GetById(Guid id)
        {
            RetrieveDataFromJsonFile();
            return accounts.Where(x => x.AccountId == id).FirstOrDefault();
        }

        public Account? GetByUsername(string username)
        {
            RetrieveDataFromJsonFile();
            return accounts.Where(x => x.Username == username).FirstOrDefault();
        }

        public void Update(Account account)
        {
            RetrieveDataFromJsonFile();

            var existingAccount = accounts.FirstOrDefault(x => x.AccountId == account.AccountId);

            if (existingAccount != null)
            {
                existingAccount.Username = account.Username;
                existingAccount.Password = account.Password;
            }

            SaveDataToJsonFile();
        }

        public bool UsernameExists(string username)
        {
            RetrieveDataFromJsonFile();
            return accounts.Where(x => x.Username == username).Any();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
