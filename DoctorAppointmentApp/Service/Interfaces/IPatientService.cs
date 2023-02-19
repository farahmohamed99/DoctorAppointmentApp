using DoctorAppointmentApp.Models;

namespace DoctorAppointmentApp.Service.Interfaces;

public interface IPatientService
{
	List<Patient> GetPatients();

	Patient GetPatientById(int id);

	bool CreatePatient(Patient patient);

	bool UpdatePatient(Patient patient);

	bool DeletePatient(int id);
}