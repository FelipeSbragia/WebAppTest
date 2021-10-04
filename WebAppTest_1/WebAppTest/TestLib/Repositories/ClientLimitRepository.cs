using System;
using System.Data.SqlClient;
using System.IO;
using TestLib.Util;

namespace TestLib.Repositories
{
    public class ClientLimitRepository
    {
        #region "Attributes"
        private readonly string currentDirectory;
        private readonly string connectionString;
        private readonly ConfigReader configReader;
        #endregion "Attributes"

        #region "Constructor"
        public ClientLimitRepository()
        {
            configReader = new ConfigReader();

            connectionString = configReader.GetConfig("ConnectionString");
            currentDirectory = Directory.GetCurrentDirectory();
        }
        #endregion "Constructor"

        #region "Methods"
        /// <summary>
        /// Method that returns the Credit Limit by Client Id
        /// </summary>
        /// <param name="clientId">Client Id</param>
        /// <returns>Credit Limit</returns>
        public decimal GetByClientId(int clientId)
        {
            decimal returnGetByClientId = 0;

            try
            {
                string queryGetCreditLimitByClientId = File.ReadAllText(currentDirectory + configReader.GetConfig("Queries:GetCreditLimitByClientId"));

                using SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = queryGetCreditLimitByClientId
                };

                SqlParameter parameter = new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = clientId };
                command.Parameters.Add(parameter);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    returnGetByClientId = decimal.Parse(reader["credit_limit"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error getting the credit limit: {ex.Message} - {ex.StackTrace}");
            }

            return returnGetByClientId;
        }
        #endregion "Methods"
    }
}
