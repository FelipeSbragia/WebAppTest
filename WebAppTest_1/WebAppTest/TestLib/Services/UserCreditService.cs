using System;
using System.Runtime.InteropServices;
using TestLib.Repositories;

namespace TestLib.Services
{
    public class UserCreditService : IDisposable
    {
        #region "Attributes"
        private bool disposed;
        private readonly SafeHandle handle;
        #endregion "Attributes"

        #region "Destructor"
        /// <summary>
        /// Public dispose method
        /// </summary>
        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error in public dispose methd: {ex.Message} - {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Private dispose method
        /// </summary>
        /// <param name="disposing">True or False</param>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposed)
                {
                    return;
                }

                if (disposing)
                {
                    handle.Dispose();
                }

                disposed = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error in private dispose method: {ex.Message} - {ex.StackTrace}");
            }
        }
        #endregion "Destructor"

        #region "Methods"
        public decimal GetCreditLimit(int clientId)
        {
            decimal returnGetCreditLimit = 0;
            ClientLimitRepository clientLimitRepository = new ClientLimitRepository();

            try
            {
                returnGetCreditLimit = clientLimitRepository.GetByClientId(clientId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error getting the credit limit: {ex.Message} - {ex.StackTrace}");
            }

            return returnGetCreditLimit;
        }
        #endregion "Methods"
    }
}