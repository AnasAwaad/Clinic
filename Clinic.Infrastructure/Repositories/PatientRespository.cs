using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories;
internal class PatientRespository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRespository(DbContext context) : base(context)
    {
    }
}
