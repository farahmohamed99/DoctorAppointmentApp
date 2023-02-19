using DoctorAppointmentApp.Models;

namespace DoctorAppointmentApp.Repository.Interfaces;

public interface IDoctorRepository
{
    List<Doctor> GetDoctors();

    Doctor GetDoctorById(int id);

    bool CreateDoctor(Doctor doctor);

    bool DoctorExists(Doctor doctor);

    bool UpdateDoctor(Doctor doctor);

    bool DeleteDoctor(Doctor doctor);

    List<int> GetDoctorSchedule(int doctorId);

    bool AddTimeSlotToDoctorSchedule(DoctorSchedule doctorSchedule);

    bool RemoveTimeSlotFromDoctorSchedule(DoctorSchedule doctorSchedule);

    bool TimeSlotExists(int timeSlotId);

    bool TimeSlotExistsForDoctor(DoctorSchedule doctorSchedule);

    DoctorSchedule GetTimeSlotById(int doctorScheduleId);
        
    bool SpecialityExists(int specialityId);
        
    bool EmailExistsForAnotherDoctor(Doctor doctor);
}