using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }


        //Ngày bao gồm cả giờ đăng ký
        [Required]
        public DateTime DateOfAppointment { get; set; }


        public int PatientId { get; set; }
        public Patient Patient { set; get; }



        //Có tình trạng là Cần xác thực, Đang chờ khám và Đã khám
        [Required]
        [StringLength(50)]
        public string Status { get; set; }

    }
}
