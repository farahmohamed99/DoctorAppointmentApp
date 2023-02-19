using DoctorAppointmentApp.Data;
using DoctorAppointmentApp.Helpers;
using DoctorAppointmentApp.Models;
using DoctorAppointmentApp.Repository.Interfaces;

namespace DoctorAppointmentApp.Repository;

public class DoctorRepository : IDoctorRepository
{
    private readonly DataContext _dataContext;

    public DoctorRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<Doctor> GetDoctors()
    {
        return _dataContext.Doctors.OrderBy(d => d.Id).ToList();
    }

    public Doctor GetDoctorById(int id)
    {
        var doctor = _dataContext.Doctors.FirstOrDefault(d => d.Id == id);
        HelperFunctions.DetachObject(_dataContext, doctor);

        return doctor;
    }

    public bool DoctorExists(Doctor doctor)
    {
        return _dataContext.Doctors.Any(d => d.Id == doctor.Id || d.Email == doctor.Email);
    }

    public bool CreateDoctor(Doctor doctor)
    {
        _dataContext.Add(doctor);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }

    public bool UpdateDoctor(Doctor doctor)
    {
        _dataContext.Update(doctor);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }

    public bool DeleteDoctor(Doctor doctor)
    {
        _dataContext.Remove(doctor);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }

    public List<int> GetDoctorSchedule(int doctorId)
    {
        var doctorSchedule = _dataContext.DoctorSchedules
            .OrderBy(ds => ds.TimeSlotId)
            .Where(ds => ds.DoctorId == doctorId).ToList();

        var timeSlotIds = new List<int>();

        if (doctorSchedule == null) return new List<int>();

        foreach (var timeSlots in doctorSchedule)
        {
            timeSlotIds.Add(timeSlots.TimeSlotId);
        }

        return timeSlotIds;

    }

    public bool AddTimeSlotToDoctorSchedule(DoctorSchedule doctorSchedule)
    {
        _dataContext.Add(doctorSchedule);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }

    public bool RemoveTimeSlotFromDoctorSchedule(DoctorSchedule doctorSchedule)
    {
        _dataContext.Remove(doctorSchedule);

        var saved = _dataContext.SaveChanges();
        return saved > 0;
    }

    public bool TimeSlotExists(int timeSlotId)
    {
        return _dataContext.TimeSlots.Any(ts => ts.Id == timeSlotId);
    }

    public bool TimeSlotExistsForDoctor(DoctorSchedule doctorSchedule)
    {
        return _dataContext.DoctorSchedules.Any(ds => ds.DoctorId == doctorSchedule.DoctorId && ds.TimeSlotId == doctorSchedule.TimeSlotId);
    }

    public DoctorSchedule GetTimeSlotById(int doctorScheduleId)
    {
        return _dataContext.DoctorSchedules.FirstOrDefault(ds => ds.Id == doctorScheduleId);
    }
        
    public bool SpecialityExists(int specialityId)
    {
        return _dataContext.Specialities.Any(s => s.Id == specialityId);
    }
        
    public bool EmailExistsForAnotherDoctor(Doctor doctor)
    {
        return _dataContext.Doctors.Any(d => d.Id != doctor.Id && d.Email == doctor.Email);
    }
}