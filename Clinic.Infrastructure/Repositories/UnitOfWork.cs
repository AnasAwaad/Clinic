using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IPatientRepository Patients { get; }
    public IDoctorScheduleRepository Schedules { get; }
    public IDoctorRepository Doctors { get; }
    public IDoctorTimeSlotRepository TimeSlots { get; }
    public IAppointmentRepository Appointments { get; }
    public IMedicalRecordRepository MedicalRecords { get; }
    public IPrescriptionRepository Prescriptions { get; }
    public IUserRepository Users { get; }
    public IRoleRepository Roles { get; }
    public IRoleClaimRepository RoleClaims { get; }
    public IGenericRepository<ClinicSettings> ClinicSettings => new GenericRepository<ClinicSettings>(_context);
    public IGenericRepository<PrescriptionItem> PrescriptionItems => new GenericRepository<PrescriptionItem>(_context);

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Patients = new PatientRespository(_context);
        Schedules = new DoctorScheduleRepository(_context);
        Doctors = new DoctorRepository(_context);
        TimeSlots = new DoctorTimeSlotRepository(_context);
        Appointments = new AppointmentRepository(_context);
        MedicalRecords = new MedicalRecordRepository(_context);
        Prescriptions = new PrescriptionRepository(_context);
        Users = new UserRepository(_context);
        Roles = new RoleRepository(_context);
        RoleClaims = new RoleClaimRepository(_context);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}