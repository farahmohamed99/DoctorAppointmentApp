namespace DoctorAppointmentApp.DTOs
{
	public class AppointmentDto
	{
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorScheduleId { get; set; }
        public bool IsPaid { get; set; } = false;
    }
}

