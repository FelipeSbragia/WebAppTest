using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestLib.Models
{
    public class User
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool HasCreditLimit { get; set; }
        public decimal CreditLimit { get; set; }
    }
}
