using DoctorAppointmentApp.Models;
using DoctorAppointmentApp.Repository.Interfaces;
using DoctorAppointmentApp.Service.Interfaces;
using static DoctorAppointmentApp.Constants.Constants;

namespace DoctorAppointmentApp.Service;

public class PatientService : IPatientService
{ 
    private readonly IPatientRepository _patientRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public PatientService(IPatientRepository patientRepository, IAppointmentRepository appointmentRepository)
    {
        _patientRepository = patientRepository;
        _appointmentRepository = appointmentRepository;
    }

    public List<Patient> GetPatients()
    {
        var patients = _patientRepository.GetPatients();
        return patients;
    }

    public Patient GetPatientById(int id)
    {
        var patient = _patientRepository.GetPatientById(id);

        if (patient == null)
        {
            throw new Exception("Patient does not exist");
        }

        return patient;
    }

    public bool CreatePatient(Patient patient)
    {
        if (patient == null)
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        if (_patientRepository.PatientExists(patient))
        {
            throw new BadHttpRequestException("Patient already exists");
        }

        if (!Enum.IsDefined(typeof(Genders), patient.Gender))
        {
            throw new BadHttpRequestException("Invalid gender");
        }

        if (!_patientRepository.CreatePatient(patient))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return true;
    }

    public bool UpdatePatient(Patient patient)
    {
        if (patient == null)
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        if (_patientRepository.GetPatientById(patient.Id) == null)
        {
            throw new Exception("Patient does not exist");
        }
            
        if (_patientRepository.EmailExistsForAnotherPatient(patient))
        {
            throw new BadHttpRequestException("Email exists for another patient");
        }

        if (!Enum.IsDefined(typeof(Genders), patient.Gender))
        {
            throw new BadHttpRequestException("Invalid gender");
        }

        if (!_patientRepository.UpdatePatient(patient))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return true;
    }

    public bool DeletePatient(int id)
    {
        var patient = _patientRepository.GetPatientById(id);

        if (patient == null)
        {
            throw new Exception("Patient does not exist");
        }

        if (_appointmentRepository.GetAppointmentsForPatient(id).ToList().Count != 0)
        {
            throw new BadHttpRequestException("Appointment exists for the selected patient!");
        }

        if (!_patientRepository.DeletePatient(patient))
        {
            throw new BadHttpRequestException("Something went wrong");
        }
            
        return true;
    }
}