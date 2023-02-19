using DoctorAppointmentApp.Models;

namespace DoctorAppointmentApp.Service.Interfaces;

public interface IDoctorService
{
	List<Doctor> GetDoctors();

	Doctor GetDoctorById(int id);

	bool CreateDoctor(Doctor doctor);

	bool UpdateDoctor(Doctor doctor);

	bool DeleteDoctor(int id);

	List<int> GetDoctorSchedule(int id);

	bool AddTimeSlotToDoctorSchedule(DoctorSchedule doctorSchedule);

	bool RemoveTimeSlotFromDoctorSchedule(int doctorScheduleId);
}