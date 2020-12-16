using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class GeneralStatisticDto
    {
        public int NumOfPatient { set; get; }
        public int NumOfExam { set; get; }
        public long ExamRevenue { set; get; }

        public long TotalRevenue { set; get; }

    }
}
