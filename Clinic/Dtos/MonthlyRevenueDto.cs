using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class MonthlyRevenueDto
    {
        public DateTime Date { get; set; }
        public long Revenue { get; set; }
        public double Percent { get; set; }
    }
}
