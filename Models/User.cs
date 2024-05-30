using academ_sync_back.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using academ_sync_back.validations;

namespace academ_sync_back.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [UserRoleValidation]
        public string Role { get; set; }
    }
}
