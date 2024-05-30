using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace academ_sync_back.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Matter { get; set; }

        [Required]
        [RegularExpression("^[2-4][0-9]{7}$")] 
        public string PhoneNumber { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
