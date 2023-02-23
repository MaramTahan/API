using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace westcoast_education.web.ViewModels.Students;

    public class StudentPostViewModel
    {
        [Required(ErrorMessage = "FirstName required")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "LastName required")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "PersonNu required")]
        public string personNu { get; set; }

        [Required(ErrorMessage = "Email required")]
        public string email { get; set; }

        [Required(ErrorMessage = "PhoneNumber required")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Address required")]
        public string address { get; set; }

        public List<SelectListItem> coursesTakenNames { get; set; }
        public string coursesTakenName { get; set; }
    }
