using System.ComponentModel.DataAnnotations;

namespace LTT.Models
{
    public class Signup
    {
        public int Id { get; set; }

        public string? Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

       
    }
}
