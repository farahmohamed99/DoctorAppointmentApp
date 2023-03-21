using DoctorAppointmentApp.Data;
using DoctorAppointmentApp.Helpers;
using DoctorAppointmentApp.Models;
using DoctorAppointmentApp.Repository.Interfaces;

namespace DoctorAppointmentApp.Repository;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly DataContext _dataContext;

    public AppointmentRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<Appointment> GetAppointments(int? doctorId, int? patientId, bool? isPaid)
    {
        if (doctorId == null
            && patientId == null
            && isPaid == null)
        {
            return _dataContext.Appointments.OrderBy(a => a.Id).ToList();
        }

        var appointments = _dataContext.Appointments as IQueryable<Appointment>;
            
        if (doctorId != null)
        {
            appointments = GetAppointmentsForDoctor(doctorId);
        }
            
        if (patientId != null)
        {
            appointments = appointments.Where(a => a.PatientId == patientId);
        }
            
        if (isPaid != null)
        {
            appointments = appointments.Where(a => a.IsPaid == isPaid);
        }

        return appointments.ToList();
    }

    public Appointment GetAppointmentById(int id)
    {
        var appointment = _dataContext.Appointments.FirstOrDefault(a => a.Id == id);
        HelperFunctions.DetachObject(_dataContext, appointment);

        return appointment;
    }

    public IQueryable<Appointment> GetAppointmentsForDoctor(int? doctorId)
    {
        return from appointments in _dataContext.Set<Appointment>()
            join doctorSchedules in _dataContext.Set<DoctorSchedule>()
                on appointments.DoctorScheduleId equals doctorSchedules.Id
            where doctorSchedules.DoctorId == doctorId
            select new Appointment
            {
                Id = appointments.Id,
                PatientId = appointments.PatientId,
                DoctorScheduleId = appointments.DoctorScheduleId,
                IsPaid = appointments.IsPaid
            };
    }

    public IQueryable<Appointment> GetAppointmentsForPatient(int? patientId)
    {
        return _dataContext.Appointments.Where(a => a.PatientId == patientId);
    }

    public bool AppointmentExists(Appointment appointment)
    {
        return _dataContext.Appointments.Any(a => a.DoctorScheduleId == appointment.DoctorScheduleId || a.Id == appointment.Id);
    }

    public bool CreateAppointment(Appointment appointment)
    {
        _dataContext.Add(appointment);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }

    public bool UpdateAppointmentStatus(Appointment appointment)
    {
        _dataContext.Update(appointment);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }

    public bool DeleteAppointment(Appointment appointment)
    {
        _dataContext.Remove(appointment);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }
}
