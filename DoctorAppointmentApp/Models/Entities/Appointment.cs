namespace DoctorAppointmentApp.Models
{
	public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorScheduleId { get; set; }
        public bool IsPaid { get; set; }

        public Patient Patient { get; set; }
        public DoctorSchedule DoctorSchedule { get; set; }

    }  
}

