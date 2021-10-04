using System;
using TestLib.Models;
using TestLib.Repositories;
using TestLib.Util;

namespace TestLib.Services
{
    public class UserService
    {
        public bool AddUser(User user, int clientId)
        {
            bool returnAddUser = false;
            DataValidator dataValidator = new DataValidator();
            ClientRepository clientRepository = new ClientRepository();
            UserRepository userRepository = new UserRepository();

            try
            {
                user.Client = clientRepository.GetById(clientId);

                if (dataValidator.ValidateName(user.FirstName, user.LastName) &&
                    dataValidator.ValidateEmail(user.EmailAddress) &&
                    dataValidator.ValidateAge(user.DateOfBirth) &&
                    dataValidator.ValidateCreditLimit(user))
                {
                    returnAddUser = userRepository.AddUser(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error adding a user: {ex.Message} - {ex.StackTrace}");
            }

            return returnAddUser;
        }
    }
}
