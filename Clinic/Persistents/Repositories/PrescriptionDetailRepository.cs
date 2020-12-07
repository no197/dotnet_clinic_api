using Clinic.Core;
using Clinic.Extensions;
using Clinic.Models;
using Clinic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinic.Persistents
{
    public class PrescriptionDetailRepository : IPrescriptionDetailRepository
    {
        private readonly ClinicDbContext context;

        public PrescriptionDetailRepository(ClinicDbContext context)
        {
            this.context = context;
        }




        public void Add(PrescriptionDetail prescriptionDetail)
        {
            context.PrescriptionDetails.Add(prescriptionDetail);
        }


        public void Update(PrescriptionDetail PrescriptionDetail)
        {
            context.PrescriptionDetails.Update(PrescriptionDetail);
        }

        public void Remove(PrescriptionDetail PrescriptionDetail)
        {
            context.PrescriptionDetails.Remove(PrescriptionDetail);
        }

       
        
    }
}
