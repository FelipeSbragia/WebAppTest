using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestLib.Models
{
    public class ClientLimits
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public decimal CreditLimit { get; set; }
    }
}
