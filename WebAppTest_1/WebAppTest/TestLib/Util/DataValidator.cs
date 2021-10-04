using System;
using TestLib.Models;
using TestLib.Services;

namespace TestLib.Util
{
    public class DataValidator
    {
        #region "Methods"
        /// <summary>
        /// Validate user name
        /// </summary>
        /// <param name="firstName">User's First Name</param>
        /// <param name="lastName">User's Last Name</param>
        /// <returns>True or False</returns>
        public bool ValidateName(string firstName, string lastName)
        {
            bool returnValidateName = false;

            try
            {
                returnValidateName = !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error validating the user name: {ex.Message} - {ex.StackTrace}");
            }

            return returnValidateName;
        }

        /// <summary>
        /// Validate user email
        /// </summary>
        /// <param name="email">User's E-mail</param>
        /// <returns>True or False</returns>
        public bool ValidateEmail(string email)
        {
            bool returnValidateEmail = false;

            try
            {
                returnValidateEmail = email.Contains("@") && email.Contains(".");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error validating the user email: {ex.Message} - {ex.StackTrace}");
            }

            return returnValidateEmail;
        }

        /// <summary>
        /// Validate user age
        /// </summary>
        /// <param name="dateOfBirth">User's Date of Birth</param>
        /// <returns>True or False</returns>
        public bool ValidateAge(DateTime dateOfBirth)
        {
            bool returnValidateAge = false;

            try
            {
                DateTime now = DateTime.Now;
                int age = now.Year - dateOfBirth.Year;
                if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                {
                    age--;
                }

                returnValidateAge = age >= 21;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error validating the user age: {ex.Message} - {ex.StackTrace}");
            }

            return returnValidateAge;
        }

        /// <summary>
        /// Validate credit limit
        /// </summary>
        /// <param name="user">Object User</param>
        /// <returns>True or False</returns>
        public bool ValidateCreditLimit(User user)
        {
            bool returnValidateCreditLimit = false;
            UserCreditService userCreditService = new UserCreditService();

            try
            {
                user.HasCreditLimit = user.Client.Name != "VeryImportantClient";
                user.CreditLimit = userCreditService.GetCreditLimit(user.Client.Id);

                returnValidateCreditLimit = !user.HasCreditLimit || user.CreditLimit >= 500;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error validating the credit limit: {ex.Message} - {ex.StackTrace}");
            }

            return returnValidateCreditLimit;
        }
        #endregion "Methods"
    }
}
