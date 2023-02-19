using DoctorAppointmentApp.Models;
using DoctorAppointmentApp.Repository.Interfaces;

namespace DoctorAppointmentApp.Service;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository,
        IDoctorRepository doctorRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
    }
        
    public List<Appointment> GetAppointments(int? doctorId, int? patientId, bool? isPaid)
    {
        var appointments = _appointmentRepository.GetAppointments(doctorId, patientId, isPaid);

        if (appointments.GetType() != typeof(List<Appointment>))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return appointments;
    }

    public Appointment GetAppointmentById(int id)
    {
        var appointment = _appointmentRepository.GetAppointmentById(id);

        if (appointment == null)
        {
            throw new Exception("Appointment does not exist");
        }

        return appointment;
    }

    public bool CreateAppointment(Appointment appointment)
    {
        if (appointment == null)
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        if (_appointmentRepository.AppointmentExists(appointment))
        {
            throw new Exception("Appointment already exists");
        }

        if (_patientRepository.GetPatientById(appointment.PatientId) == null)
        {
            throw new Exception("Patient does not exist");
        }

        if (_doctorRepository.GetTimeSlotById(appointment.DoctorScheduleId) == null)
        {
            throw new Exception("Time slot does not exist");
        }

        if (!_appointmentRepository.CreateAppointment(appointment))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return true;
    }

    public bool UpdateAppointmentStatus(Appointment appointment)
    {

        if (appointment == null)
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        if (!_appointmentRepository.AppointmentExists(appointment))
        {
            throw new Exception("Appointment does not exist");
        }

        var updatedAppointment = _appointmentRepository.GetAppointmentById(appointment.Id);
        updatedAppointment.IsPaid = appointment.IsPaid;

        if (!_appointmentRepository.UpdateAppointmentStatus(updatedAppointment))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return true;
    }

    public bool DeleteAppointment(int appointmentId)
    {
        var appointment = _appointmentRepository.GetAppointmentById(appointmentId);

        if (appointment == null)
        {
            throw new Exception("Appointment does not exist");
        }

        if (!_appointmentRepository.DeleteAppointment(appointment))
        {
            throw new BadHttpRequestException("Something went wrong");
        }
            
        return true;
    }
}