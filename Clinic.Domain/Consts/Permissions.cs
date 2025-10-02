using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Consts;
public static class Permissions
{
    public static string Type { get; } = "permissions";

    public const string GetTimeSlots = "timeslots:read";
    public const string AddTimeSlots = "timeslots:add";
    public const string UpdateTimeSlots = "timeslots:update";
    public const string DeleteTimeSlots = "timeslots:delete";


    public const string GetMedicalRecords = "medicalrecords:read";
    public const string AddMedicalRecords = "medicalrecords:add";
    public const string UpdateMedicalRecords = "medicalrecords:update";
    public const string DeleteMedicalRecords = "medicalrecords:delete";

    public const string GetPrescriptions = "prescriptions:read";
    public const string AddPrescriptions = "prescriptions:add";
    public const string UpdatePrescriptions = "prescriptions:update";
    public const string DeletePrescriptions = "prescriptions:delete";

    public const string GetAppointments = "appointments:read";
    public const string GetOwnAppointments = "appointments:read-own";
    public const string AddAppointments = "appointments:add";
    public const string UpdateAppointments = "appointments:update";
    public const string CancelAppointments = "appointments:cancel";
    public const string DeleteAppointments = "appointments:delete";

    public const string GetUsers = "users:read";
    public const string AddUsers = "users:add";
    public const string UpdateUsers = "users:update";


    public const string GetRoles = "roles:read";
    public const string AddRoles = "roles:add";
    public const string UpdateRoles = "roles:update";


    public const string GetResults = "results:read";

    public static IList<string?> GetAllPermissions() =>
        typeof(Permissions).GetFields().Select(x => x.GetValue(x) as string).ToList().ToList();
}
