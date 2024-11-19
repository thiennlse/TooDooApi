using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class ApplicationUsers : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? Dob { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }
        [JsonIgnore]
        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
        [JsonIgnore]
        public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
        public ICollection<UserSubcription> UserSubcriptions { get; set; } = new List<UserSubcription>();
    }
}
