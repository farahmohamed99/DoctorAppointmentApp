using DoctorAppointmentApp.Models;

namespace DoctorAppointmentApp.Repository.Interfaces;

public interface IPatientRepository
{
	List<Patient> GetPatients();

	Patient GetPatientById(int id);

	bool PatientExists(Patient patient);

	bool CreatePatient(Patient patient);

	bool UpdatePatient(Patient patient);

	bool DeletePatient(Patient patient);
	bool EmailExistsForAnotherPatient(Patient patient);
}