using DoctorAppointmentApp.Models;

namespace DoctorAppointmentApp.Repository.Interfaces;

public interface IAppointmentRepository
{
	List<Appointment> GetAppointments(int? doctorId, int? patientId, bool? isPaid);
		
	Appointment GetAppointmentById(int id);
		
	IQueryable<Appointment> GetAppointmentsForDoctor(int? doctorId);

	IQueryable<Appointment> GetAppointmentsForPatient(int? patientId);

	bool AppointmentExists(Appointment appointment);

	bool CreateAppointment(Appointment appointment);

	bool UpdateAppointmentStatus(Appointment appointment);

	bool DeleteAppointment(Appointment appointment);
}