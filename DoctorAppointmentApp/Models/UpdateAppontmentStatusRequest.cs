namespace DoctorAppointmentApp.Models
{
	public class UpdateAppointmentStatusRequest
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
    }
}

