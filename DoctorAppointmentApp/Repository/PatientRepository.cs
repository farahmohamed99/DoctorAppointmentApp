using DoctorAppointmentApp.Data;
using DoctorAppointmentApp.Helpers;
using DoctorAppointmentApp.Models;
using DoctorAppointmentApp.Repository.Interfaces;

namespace DoctorAppointmentApp.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly DataContext _dataContext;

    public PatientRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<Patient> GetPatients()
    {
        return _dataContext.Patients.OrderBy(p => p.Id).ToList();
    }

    public Patient GetPatientById(int id)
    {
        var patient = _dataContext.Patients.Where(p => p.Id == id).FirstOrDefault();
        HelperFunctions.DetachObject(_dataContext, patient);

        return patient;
    }

    public bool PatientExists(Patient patient)
    {
        return _dataContext.Patients.Any(p => p.Id == patient.Id || p.Email == patient.Email);
    }

    public bool CreatePatient(Patient patient)
    {
        _dataContext.Add(patient);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }

    public bool UpdatePatient(Patient patient)
    {
        //Detach object was not working here
        _dataContext.Update(patient);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }

    public bool DeletePatient(Patient patient)
    {
        _dataContext.Remove(patient);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }

    public bool EmailExistsForAnotherPatient(Patient patient)
    {
        return _dataContext.Patients.Any(p => p.Id != patient.Id && p.Email == patient.Email);
    }
}