namespace DoctorAppointmentApp.Models
{
	public class DoctorSchedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int TimeSlotId { get; set; }

        public Doctor Doctor { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }
}

