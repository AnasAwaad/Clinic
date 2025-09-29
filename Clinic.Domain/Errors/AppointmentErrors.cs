using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Errors;
public static class AppointmentErrors
{
    public static readonly Error AppointmentNotFound = new("Appointment.AppointmentNotFound", "Appointment with the given id not found", StatusCodes.Status404NotFound);
    public static readonly Error TimeSlotAlreadyBooked = new("Appointment.TimeSlotAlreadyBooked", "Time slot already booked", StatusCodes.Status400BadRequest);
    public static readonly Error ActiveAppointmentExists = new("Appointment.ActiveAppointmentExists", "The pateint already has active appointment", StatusCodes.Status400BadRequest);
    public static readonly Error AlreadyCancelled = new("Appointment.AlreadyCancelled", "Appintment is aleardy canceled", StatusCodes.Status400BadRequest);
}