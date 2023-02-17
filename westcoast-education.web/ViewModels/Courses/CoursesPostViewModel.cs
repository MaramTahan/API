
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace westcoast_education.web.ViewModels;

    public class CoursesPostViewModel
    {
        [Required(ErrorMessage = "CourseNumber required")]
        [DisplayName("courseNumber")]
        public string courseNumber { get; set; }

        public List<SelectListItem> nameOfCourse { get; set; }

        [Required(ErrorMessage = "StartDate required")]
        [DisplayName("startDate")]
        public string startDate { get; set; }

        [Required(ErrorMessage = "EndDate required")]
        [DisplayName("endDate")]
        public string endDate { get; set; }

        [Required(ErrorMessage = "Teacher required")]
        [DisplayName("teacher")]
        public string teacher { get; set; }

        [Required(ErrorMessage = "PlaceOfStudy required")]
        [DisplayName("placeStudy")]
        public string placeStudy { get; set; }
    }
