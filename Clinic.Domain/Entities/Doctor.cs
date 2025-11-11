namespace Clinic.Domain.Entities;
public class Doctor : ApplicationUser
{
    public string Specialization { get; set; } = string.Empty;
    public int YearOfExperience { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
