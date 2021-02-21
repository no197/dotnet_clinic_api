using Clinic.Models;
using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core
{
    public interface IPrescriptionDetailRepository
    {
        //Task<List<PrescriptionDetail>> getPrescriptions();
        void Add(PrescriptionDetail Prescription);
        void Update(PrescriptionDetail Prescription);
        void Remove(PrescriptionDetail Prescription);

    }
}
