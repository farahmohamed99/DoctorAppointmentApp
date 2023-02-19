using DoctorAppointmentApp.Models;

namespace DoctorAppointmentApp.Repository.Interfaces;

public interface IAppointmentService
{
	List<Appointment> GetAppointments(int? doctorId, int? patientId, bool? isPaid);
	
	Appointment GetAppointmentById(int id);

	bool CreateAppointment(Appointment appointment);

	bool UpdateAppointmentStatus(Appointment appointment);

	bool DeleteAppointment(int appointmentId);
}