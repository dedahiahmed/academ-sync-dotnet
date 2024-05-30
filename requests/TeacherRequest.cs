using System.ComponentModel.DataAnnotations;

namespace academ_sync_back.requests
{
    public class TeacherRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [RegularExpression("^[2-4][0-9]{7}$")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Matter { get; set; }
    }
}
