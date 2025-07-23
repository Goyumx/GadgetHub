using System.ComponentModel.DataAnnotations;

namespace GadgetHub.API.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } // For now, we'll store plain text (we’ll improve it later)

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
