namespace westcoast_education2.api.ViewModels;

    public class TeachersListViewModel
    {
        public int TUserId{ get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string coursesTaughtName { get; set; }
    }
