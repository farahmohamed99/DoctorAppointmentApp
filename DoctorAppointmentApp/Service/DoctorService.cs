using DoctorAppointmentApp.Models;
using DoctorAppointmentApp.Repository.Interfaces;
using DoctorAppointmentApp.Service.Interfaces;
using static DoctorAppointmentApp.Constants.Constants;

namespace DoctorAppointmentApp.Service;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public DoctorService(IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository)
    {
        _doctorRepository = doctorRepository;
        _appointmentRepository = appointmentRepository;
    }

    public List<Doctor> GetDoctors()
    {
        var doctors = _doctorRepository.GetDoctors();
        return new List<Doctor>(doctors);
    }

    public Doctor GetDoctorById(int id)
    {
        var doctor = _doctorRepository.GetDoctorById(id);

        if (doctor == null)
        {
            throw new Exception("Doctor does not exist");
        }

        return doctor;
    }

    public bool CreateDoctor(Doctor doctor)
    {
        if (doctor == null)
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        if (!_doctorRepository.SpecialityExists(doctor.SpecialityId))
        {
            throw new Exception("Speciality does not exist");
        }
            
        if (!Enum.IsDefined(typeof(Genders), doctor.Gender))
        {
            throw new BadHttpRequestException("Invalid gender");
        }

        if (_doctorRepository.DoctorExists(doctor))
        {
            throw new BadHttpRequestException("Doctor already exists");
        }

        if (!_doctorRepository.CreateDoctor(doctor))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return true;
    }

    public bool UpdateDoctor(Doctor doctor)
    {
        if (doctor == null)
        {
            throw new BadHttpRequestException("Something went wrong");
        }
            
        if (_doctorRepository.EmailExistsForAnotherDoctor(doctor))
        {
            throw new BadHttpRequestException("Email exists for another doctor");
        }

        if (!_doctorRepository.SpecialityExists(doctor.SpecialityId))
        {
            throw new Exception("Speciality does not exist");
        }

        if (!Enum.IsDefined(typeof(Genders), doctor.Gender))
        {
            throw new BadHttpRequestException("Invalid gender");
        }

        if (!_doctorRepository.UpdateDoctor(doctor))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return true;
    }

    public bool DeleteDoctor(int id)
    {
        var doctor = _doctorRepository.GetDoctorById(id);

        if (doctor == null)
        {
            throw new Exception("Doctor does not exist");
        }

        if (_appointmentRepository.GetAppointmentsForDoctor(id).ToList().Count != 0)
        {
            throw new BadHttpRequestException("Appointment exists for the selected doctor!");
        }

        if (!_doctorRepository.DeleteDoctor(doctor))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return true;
    }

    public List<int> GetDoctorSchedule(int id)
    {
        if (_doctorRepository.GetDoctorById(id) == null)
        {
            throw new Exception("Doctor does not exist");
        }

        var doctorSchedule = _doctorRepository.GetDoctorSchedule(id);

        if (doctorSchedule.GetType() != typeof(List<int>))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return doctorSchedule;
    }

    public bool AddTimeSlotToDoctorSchedule(DoctorSchedule doctorSchedule)
    {
        if (doctorSchedule == null)
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        if (_doctorRepository.GetDoctorById(doctorSchedule.DoctorId) == null)
        {
            throw new Exception("Doctor does not exist");
        }

        if (!_doctorRepository.TimeSlotExists(doctorSchedule.TimeSlotId))
        {
            throw new Exception("Time slot does not exist");
        }

        if (_doctorRepository.TimeSlotExistsForDoctor(doctorSchedule))
        {
            throw new BadHttpRequestException("Time slot already exists for the doctor");
        }

        if (!_doctorRepository.AddTimeSlotToDoctorSchedule(doctorSchedule))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return true;
    }

    public bool RemoveTimeSlotFromDoctorSchedule(int doctorScheduleId)
    {
        var doctorSchedule = _doctorRepository.GetTimeSlotById(doctorScheduleId);

        if (doctorSchedule == null)
        {
            throw new Exception("Time slot does not exist for the doctor");
        }

        var doctorAppointments = _appointmentRepository.GetAppointmentsForDoctor(doctorSchedule.DoctorId).ToList();
        foreach (var appointment in doctorAppointments)
        {
            if (appointment.DoctorScheduleId == doctorSchedule.Id)
            {
                throw new BadHttpRequestException("Appointment exists in the time slot of the selected doctor!");
            }
        }

        if (!_doctorRepository.RemoveTimeSlotFromDoctorSchedule(doctorSchedule))
        {
            throw new BadHttpRequestException("Something went wrong");
        }

        return true;
    }
}