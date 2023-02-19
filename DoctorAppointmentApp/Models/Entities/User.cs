using static DoctorAppointmentApp.Constants.Constants;

namespace DoctorAppointmentApp.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { set; get; }
        public string Email { set; get; }
        public Genders Gender { set; get; }

    }
}

