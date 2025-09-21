using Clinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IUnitOfWork : IDisposable
{
    IPatientRepository Patients { get; }
    Task<int> SaveAsync();
}



