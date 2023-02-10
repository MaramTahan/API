
using System.ComponentModel.DataAnnotations;

namespace westcoast_education2.api.ViewModels;

    public class TeacherAddViewModel
    {
        [Required(ErrorMessage = "UserId required")]
        public int TUserId{ get; set; }

        [Required(ErrorMessage = "FirstName required")]
        public string? firstName { get; set; }

        [Required(ErrorMessage = "LastName required")]
        public string? lastName { get; set; }

        [Required(ErrorMessage = "Email required")]
        public string? email { get; set; }

        [Required(ErrorMessage = "PhoneNumber required")]
        public string? phoneNumber { get; set; }

        [Required(ErrorMessage = "Address required")]
        public string? address { get; set; }
        
        public string coursesTaughtName { get; set; } = "";
    }
