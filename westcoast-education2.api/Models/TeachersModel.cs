using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoast_education2.api.Models;

    public class TeachersModel
    {
        [Key]
        public int TUserId{ get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? address { get; set; }
        public int coursesTaughtId { get; set; }

        //The one side..
        //Navigation properties
        //Create connections between related classes..
        [ForeignKey ("coursesTaughtId")]
        public CourseNameModel courseName { get; set; } = new CourseNameModel();
    }
