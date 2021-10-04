using System;

namespace TestLib.Models
{
    public class User
    {
        public Client Client { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool HasCreditLimit { get; set; }
        public decimal CreditLimit { get; set; }
    }
}
