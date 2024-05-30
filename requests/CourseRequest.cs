using academ_sync_back.Enums;
using academ_sync_back.validations;
using System.ComponentModel.DataAnnotations;

namespace academ_sync_back.requests
{
    public class CourseRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [CourseTypeValidation]
        public string Type { get; set; }

        [Required]
        [SemesterTypeValidation]
        public string Semester { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public string[] Files { get; set; }
    }
}
