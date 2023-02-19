using DoctorAppointmentApp.Data;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentApp.Helpers
{
	public static class HelperFunctions
    {
    
        public static void DetachObject(DataContext dataContext, Object obj)
        {
            if (obj != null)
            {
                dataContext.Entry(obj).State = EntityState.Detached;
            }
        }
    }
}

