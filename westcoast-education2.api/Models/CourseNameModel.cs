
using System.ComponentModel.DataAnnotations;

namespace westcoast_education2.api.Models;

    public class CourseNameModel
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }

        //This is the many-side..
        public ICollection<CoursesModel> Courses { get; set; }
        public ICollection<StudentsModel> StudentsCourses { get; set; }
        public ICollection<TeachersModel> TeachersCourses { get; set; }

    }
