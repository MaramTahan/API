
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace westcoast_education.web.ViewModels.Teachers;

    public class TeacherPostViewModel
    {
        [Required(ErrorMessage = "FirstName required")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "LastName required")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Email required")]
        public string email { get; set; }

        [Required(ErrorMessage = "PhoneNumber required")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Address required")]
        public string address { get; set; }
        
        public List<SelectListItem> coursesTaughtNames { get; set; }
        public string coursesTaughtName { get; set; }
    }
