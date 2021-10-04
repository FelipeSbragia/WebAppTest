using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestLib.Models
{
    public class Client
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}