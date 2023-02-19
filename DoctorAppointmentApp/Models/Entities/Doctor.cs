namespace DoctorAppointmentApp.Models
{
    public class Doctor : User
    {
        public int SpecialityId { get; set; }
        public long Fees { get; set; }

        public Speciality Speciality { get; set; }
    }
}