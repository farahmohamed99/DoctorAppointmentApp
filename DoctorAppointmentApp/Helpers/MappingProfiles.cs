using AutoMapper;
using DoctorAppointmentApp.DTOs;
using DoctorAppointmentApp.Models;

namespace DoctorAppointmentApp.Helpers
{
	public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();

            CreateMap<DoctorSchedule, AddTimeSlotToDoctorScheduleRequest>();
            CreateMap<AddTimeSlotToDoctorScheduleRequest, DoctorSchedule>();

            CreateMap<Appointment, AppointmentDto>();
            CreateMap<AppointmentDto, Appointment>();

            CreateMap<UpdateAppointmentStatusRequest, Appointment>();
        }
    }
}

