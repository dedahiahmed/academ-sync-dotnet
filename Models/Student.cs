using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace academ_sync_back.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Sector { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
