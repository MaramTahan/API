
using System.ComponentModel.DataAnnotations;
using westcoast_education2.api.Models;

namespace westcoast_education2.api.ViewModels;

    public class CourseAddViewModel
    {
        [Required(ErrorMessage = "CourseId required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "CourseNumber required")]
        public string? courseNumber { get; set; }

        [Required(ErrorMessage = "NameOfCourse required")]
        public string nameOfCourse { get; set; } = "";

        [Required(ErrorMessage = "StartDate required")]
        public string? startDate { get; set; }

        [Required(ErrorMessage = "EndDate required")]
        public string? endDate { get; set; }

        [Required(ErrorMessage = "Teacher required")]
        public string? teacher { get; set; }

        [Required(ErrorMessage = "PlaceOfStudy required")]
        public string? placeStudy { get; set; }
    }