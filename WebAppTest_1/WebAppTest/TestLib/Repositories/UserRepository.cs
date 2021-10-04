using System;
using System.Data.SqlClient;
using System.IO;
using TestLib.Models;
using TestLib.Util;

namespace TestLib.Repositories
{
    public class UserRepository
    {
        #region "Attributes"
        private readonly string currentDirectory;
        private readonly string connectionString;
        private readonly ConfigReader configReader;
        #endregion "Attributes"

        #region "Constructor"
        public UserRepository()
        {
            configReader = new ConfigReader();

            connectionString = configReader.GetConfig("ConnectionString");
            currentDirectory = Directory.GetCurrentDirectory();
        }
        #endregion "Constructor"

        #region "Methods"
        /// <summary>
        /// Method that add a User
        /// </summary>
        /// <param name="user">Object User</param>
        /// <returns>True or False</returns>
        public bool AddUser(User user)
        {
            bool addUser = false;

            try
            {
                string queryAddUser = File.ReadAllText(currentDirectory + configReader.GetConfig("Queries:AddUser"));

                using SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = queryAddUser
                };

                command.Parameters.Add(new SqlParameter("@clientId", System.Data.SqlDbType.Int) { Value = user.Client.Id });
                command.Parameters.Add(new SqlParameter("@DateOfBirth", System.Data.SqlDbType.DateTime) { Value = user.DateOfBirth });
                command.Parameters.Add(new SqlParameter("@FirstName", System.Data.SqlDbType.VarChar) { Value = user.FirstName });
                command.Parameters.Add(new SqlParameter("@LastName", System.Data.SqlDbType.VarChar) { Value = user.LastName });
                command.Parameters.Add(new SqlParameter("@EmailAddress", System.Data.SqlDbType.VarChar) { Value = user.EmailAddress });
                command.Parameters.Add(new SqlParameter("@HasCreditLimit", System.Data.SqlDbType.TinyInt) { Value = user.HasCreditLimit ? 1 : 0 });
                command.Parameters.Add(new SqlParameter("@CreditLimit", System.Data.SqlDbType.Decimal) { Value = user.CreditLimit });

                connection.Open();
                command.ExecuteNonQuery();

                addUser = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error adding a user: {ex.Message} - {ex.StackTrace}");
            }

            return addUser;
        }
        #endregion "Methods"
    }
}