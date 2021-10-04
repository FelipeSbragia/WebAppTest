using System;
using TestLib.Models;
using TestLib.Services;

namespace TestApp
{
    class Program
    {
        //Option 1: Testing with mock data, saving in SQL Server Database
        static void Main(string[] args)
        {
            AddUser(args);
        }

        public static void AddUser(string[] args)
        {
            User user = new User()
            {
                FirstName = "Felipe",
                LastName = "Sbragia",
                EmailAddress = "fmsbragia@gmail.com",
                DateOfBirth = new DateTime(1987, 06, 28)
            };

            UserService userService = new UserService();
            bool result = userService.AddUser(user, 1);
            Console.WriteLine($"Adding user {user.FirstName} {user.LastName} was {(result ? "successfull" : "unsuccessful")}");
        }
    }
}
