using westcoast_education2.api.Models;

namespace westcoast_education2.api.ViewModels;

    public class CoursesListViewModel
    {
        public int Id { get; set; }
        public string? courseNumber { get; set; }
        public string nameOfCourse { get; set; } = "";
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public string? teacher { get; set; }
        public string? placeStudy { get; set; }
    }
