using AccountManagementModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagementDataService
{
    public class AccountDBData : IAccountDataService
    {

        //connectionString
        private string connectionString
        = "Data Source =localhost\\SQLEXPRESS; Initial Catalog = AccountMgmtINTEG; Integrated Security = True; TrustServerCertificate=True;";

        private SqlConnection sqlConnection;
        public AccountDBData()
        {
            sqlConnection = new SqlConnection(connectionString);

            AddSeeds();
        }

        private void AddSeeds()
        {
            var existing = GetAccounts();

            if (existing.Count == 0)
            {
                Account adminAccount = new Account { AccountId = Guid.NewGuid(), Username = "admin", Password = "admin123!" };
                Account guestAccount = new Account { AccountId = Guid.NewGuid(), Username = "guest", Password = "guest123!" };
               Account userAccount = new Account { AccountId = Guid.NewGuid(), Username = "user", Password = "user123!" };

                Add(adminAccount);
                Add(guestAccount);
                Add(userAccount);
            }
        }
        public void Add(Account account)
        {
            var insertStatement = "INSERT INTO Accounts VALUES (@AccountId, @Username, @Password)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@AccountId", account.AccountId);
            insertCommand.Parameters.AddWithValue("@Username", account.Username);
            insertCommand.Parameters.AddWithValue("@Password", account.Password);
            sqlConnection.Open();

            insertCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public List<Account> GetAccounts()
        {
            string selectStatement = "SELECT AccountId, Username, Password FROM Accounts";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var accounts = new List<Account>();

            while (reader.Read())
            {
                //deserialize

                Account account = new Account();
                account.AccountId = Guid.Parse(reader["AccountId"].ToString());
                account.Username = reader["Username"].ToString();
                account.Password = reader["Password"].ToString();

                accounts.Add(account);
            }

            sqlConnection.Close();
            return accounts;
        }

        public Account? GetById(Guid id)
        {
            var selectStatement = "SELECT AccountId, Username, Password FROM Accounts WHERE AccountId = @AccountId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@AccountId", id.ToString());
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var account = new Account();

            while (reader.Read())
            {
                account.AccountId = Guid.Parse(reader["AccountId"].ToString());
                account.Username = reader["Username"].ToString();
                account.Password = reader["Password"].ToString();
            }

            sqlConnection.Close();
            return account;
        }

        public Account? GetByUsername(string username)
        {
            var selectStatement = "SELECT AccountId, Username, Password FROM Accounts WHERE Username = @username";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@UserName", username);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var account = new Account();

            while (reader.Read())
            {
                account.AccountId = Guid.Parse(reader["AccountId"].ToString());
                account.Username = reader["Username"].ToString();
                account.Password = reader["Password"].ToString();
            }

            sqlConnection.Close();
            return account;
        }

        public void Update(Account account)
        {
            sqlConnection.Open();

            var updateStatement = $"UPDATE Accounts SET Username = @Username, Password = @Password WHERE AccountId = @AccountId";

            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);

            updateCommand.Parameters.AddWithValue("@Username", account.Username);
            updateCommand.Parameters.AddWithValue("@Password", account.Password);
            updateCommand.Parameters.AddWithValue("@AccountId", account.AccountId);
            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public void Delete(Guid accountId)
        {
            sqlConnection.Open();

            var deleteStatement = "DELETE FROM Accounts WHERE AccountId = @AccountId";

            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);

            deleteCommand.Parameters.AddWithValue("@AccountId", accountId.ToString());

            deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public bool UsernameExists(string username)
        {
            var selectStatement = "SELECT AccountId, Username, Password FROM Accounts WHERE Username = @Username";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@Username", username);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var account = new Account();

            while (reader.Read())
            {
                account.AccountId = Guid.Parse(reader["AccountId"].ToString());
                account.Username = reader["Username"].ToString();
                account.Password = reader["Password"].ToString();
            }

            sqlConnection.Close();
            return account.Username != null;
        }
    }
}
