using DoctorAppointmentApp.Data;
using DoctorAppointmentApp.Models;
using static DoctorAppointmentApp.Constants.Constants;

namespace DoctorAppointmentApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Specialities.Any())
            {
                var Specialities = new List<Speciality>()
                {
                    new Speciality()
                    {
                        Name = "Dermatology"
                    },
                    new Speciality()
                    {
                        Name = "Neurology"
                    },
                    new Speciality()
                    {
                        Name = "Surgery"
                    },
                    new Speciality()
                    {
                        Name = "Oncology"
                    }
                };
                dataContext.Specialities.AddRange(Specialities);
                dataContext.SaveChanges();
            }

            if (!dataContext.Doctors.Any())
            {
                var Doctors = new List<Doctor>()
                {
                    new Doctor()
                    {
                        FirstName = "Doctor A fn",
                        LastName = "Doctor A ln",
                        MobileNumber = "01111232334",
                        Email = "DoctorA@gmail.com",
                        Gender = Genders.Male,
                        SpecialityId = 2,
                        Fees = 200
                    },
                    new Doctor()
                    {
                        FirstName = "Doctor B fn",
                        LastName = "Doctor B ln",
                        MobileNumber = "01111232335",
                        Email = "DoctorB@gmail.com",
                        Gender = Genders.Female,
                        SpecialityId= 1,
                        Fees = 100
                    }
                };
                dataContext.Doctors.AddRange(Doctors);
                dataContext.SaveChanges();
            }
            if (!dataContext.Patients.Any())
            {
                var Patients = new List<Patient>()
                {
                    new Patient()
                    {
                        FirstName = "Patient A fn",
                        LastName = "Patient A ln",
                        MobileNumber = "01111232336",
                        Email = "PatientA@gmail.com",
                        Gender = Genders.Female
                    },
                    new Patient()
                    {
                        FirstName = "Patient B fn",
                        LastName = "Patient B ln",
                        MobileNumber = "01111232337",
                        Email = "PatientB@gmail.com",
                        Gender = Genders.Male,
                    }
                };
                dataContext.Patients.AddRange(Patients);
                dataContext.SaveChanges();
            }

            if (!dataContext.TimeSlots.Any())
            {
                // Available time slots for a year
                var TimeSlots = new List<TimeSlot>();
                var startDate = DateTime.Today.ToUniversalTime();
                var endDate = new DateTime(2024, 1, 1, 0, 0, 0).ToUniversalTime();
                for (DateTime d = startDate; d < endDate; d = d.AddDays(1))
                {
                    // Available time slots = Everyday from 10 AM to 8 PM
                    for (int j = 10; j < 20; j++)
                    {
                        TimeSlots.Add(new TimeSlot
                        {
                            From = d.AddHours(j).ToUniversalTime(),
                            To = d.AddHours(j + 1).ToUniversalTime()
                        });
                    }
                }
                dataContext.TimeSlots.AddRange(TimeSlots);
                dataContext.SaveChanges();
            }

            if (!dataContext.DoctorSchedules.Any())
            {
                var DoctorSchedules = new List<DoctorSchedule>()
                {
                    new DoctorSchedule()
                    {
                        DoctorId = 1,
                        TimeSlotId = 1
                    },
                    new DoctorSchedule()
                    {
                        DoctorId = 1,
                        TimeSlotId = 3
                    },
                    new DoctorSchedule()
                    {
                        DoctorId = 2,
                        TimeSlotId = 1
                    },
                    new DoctorSchedule()
                    {
                        DoctorId = 2,
                        TimeSlotId = 5
                    }
                };
                dataContext.DoctorSchedules.AddRange(DoctorSchedules);
                dataContext.SaveChanges();
            }

            if (!dataContext.Appointments.Any())
            {
                var Appointments = new List<Appointment>()
                {
                   new Appointment()
                   {
                       PatientId = 2,
                       DoctorScheduleId = 4,
                       IsPaid=  false
                    },
                   new Appointment()
                   {
                       PatientId = 2,
                       DoctorScheduleId = 1,
                       IsPaid=  false
                    },
                   new Appointment()
                   {
                       PatientId = 1,
                       DoctorScheduleId = 3,
                       IsPaid = true
                    },
                   new Appointment()
                   {
                       PatientId = 1,
                       DoctorScheduleId = 2,
                       IsPaid = true
                    }
                };
                dataContext.Appointments.AddRange(Appointments);
                dataContext.SaveChanges();
            }
        }
    }
}

