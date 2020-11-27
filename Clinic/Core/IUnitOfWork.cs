using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
