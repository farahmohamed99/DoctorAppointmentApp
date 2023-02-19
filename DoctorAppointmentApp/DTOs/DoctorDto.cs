using static DoctorAppointmentApp.Constants.Constants;

namespace DoctorAppointmentApp.Models
{
    public class DoctorDto 
    {
        public int Id { get; set; }
        public int SpecialityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { set; get; }
        public string Email { set; get; }
        public Genders Gender { set; get; }
        public long Fees { get; set; }

    }
}

