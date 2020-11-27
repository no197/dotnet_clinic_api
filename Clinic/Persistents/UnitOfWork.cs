using Clinic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Persistents
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClinicDbContext context;

        public UnitOfWork(ClinicDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
