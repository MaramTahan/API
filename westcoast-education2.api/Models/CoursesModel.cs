using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace westcoast_education2.api.Models;

    public class CoursesModel
    {
        [Key]
        public int Id { get; set; }
        public string courseNumber { get; set; }
        public int nameId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string teacher { get; set; }
        public string placeStudy { get; set; }
        public CourseStatusEnum status { get; set; }

       //The one side..
       //Navigation properties
       //Create connections between related classes..
        [ForeignKey ("nameId")]
        public CourseNameModel courseName { get; set; } = new CourseNameModel();

    }


