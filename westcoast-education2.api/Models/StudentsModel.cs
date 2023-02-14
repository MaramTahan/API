

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoast_education2.api.Models;

    public class StudentsModel
    {
        [Key]
        public int userId{ get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string personNu { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public int coursesTakenId { get; set; }

       //The one side..
       //Navigation properties
       //Create connections between related classes..
        [ForeignKey ("coursesTakenId")]
        public CourseNameModel courseName { get; set; } = new CourseNameModel();

    }
