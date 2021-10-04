using System;
using System.Data.SqlClient;
using System.IO;
using TestLib.Models;
using TestLib.Util;

namespace TestLib.Repositories
{
    public class ClientRepository
    {
        #region "Attributes"
        private readonly string currentDirectory;
        private readonly string connectionString;
        private readonly ConfigReader configReader;
        #endregion "Attributes"

        #region "Constructor"
        public ClientRepository()
        {
            configReader = new ConfigReader();

            connectionString = configReader.GetConfig("ConnectionString");
            currentDirectory = Directory.GetCurrentDirectory();
        }
        #endregion "Constructor"

        #region "Methods"
        /// <summary>
        /// Method that returns a Client by Id
        /// </summary>
        /// <param name="clientId">Cliente Id</param>
        /// <returns>Cliente Object</returns>
        public Client GetById(int clientId)
        {
            Client client = null;

            try
            {
                string queryGetClientById = File.ReadAllText(currentDirectory + configReader.GetConfig("Queries:GetClientById"));

                using SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = queryGetClientById
                };

                SqlParameter parameter = new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = clientId };
                command.Parameters.Add(parameter);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    client = new Client
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Name = reader["name"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error validating the user age: {ex.Message} - {ex.StackTrace}");
            }

            return client;
        }
        #endregion "Methods"
    }
}