using AutoMapper;
using Clinic.Dtos;
using Clinic.Models;
using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Mapping
{
    public class MappingProflie : Profile
    {
        public MappingProflie()
        {
            //CreateMap<Entity, DTO>
            CreateMap(typeof(QueryResult<>), typeof(QueryResultDto<>));
            CreateMap<Patient, PatientDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Medicine, MedicineDto>();
            CreateMap<EmployeeAccount, UserDto>()
                .ForMember(user => user.EmployeeId, opt => opt.MapFrom(acc => acc.Employee.EmployeeId))
                .ForMember(user => user.FullName, opt => opt.MapFrom(acc => acc.Employee.FullName))
                .ForMember(user => user.Position, opt => opt.MapFrom(acc => acc.Employee.Position));

            CreateMap<Appointment, AppointmentDto>()
               .ForMember(appDto => appDto.PatientName, opt => opt.MapFrom(app => app.Patient.FullName));

            CreateMap<Examination, ExaminationDto>()
                .ForMember(examDto => examDto.EmployeeName, opt => opt.MapFrom(exam => exam.Employee.FullName))
                .ForMember(examDto => examDto.PatientName, opt => opt.MapFrom(exam => exam.Appointment.Patient.FullName));


            CreateMap<Examination, ExaminationDetailDto>()
               .ForMember(examDto => examDto.EmployeeName, opt => opt.MapFrom(exam => exam.Employee.FullName))
               .ForMember(examDto => examDto.Patient, opt => opt.MapFrom(exam => exam.Appointment.Patient));

            CreateMap<PrescriptionDetail, PrescriptionDetailDto>()
                .ForMember(preDto => preDto.MedicineName, opt => opt.MapFrom(pre => pre.Medicine.MedicineName));

            CreateMap<PrescriptionDetail, PrescriptionDetailInVoiceDto>()
                .ForMember(preDto => preDto.MedicineName, opt => opt.MapFrom(pre => pre.Medicine.MedicineName));

            CreateMap<Invoice, InvoiceDto>()
                .ForMember(invDto => invDto.PatientName, opt => opt.MapFrom(inv => inv.Examination.Appointment.Patient.FullName));
               

            CreateMap<Invoice, InvoiceDetailDto>()
                .ForMember(invDto => invDto.PrescriptionDetailsPrice, opt => opt.MapFrom(inv => inv.Examination.PrescriptionDetails))
                .ForMember(invDto => invDto.Patient, opt => opt.MapFrom(inv => inv.Examination.Appointment.Patient))
                .ForMember(invDto => invDto.EmployeeName, opt => opt.MapFrom(inv => inv.Examination.Employee.FullName));
            //không chắc là cần :))))
            CreateMap<Appointment, AppointmentSaveDto>();
           

            //Create<DTO, Entity>
            CreateMap<PatientDto, Patient>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<MedicineDto, Medicine>();
            CreateMap<RegisterDto, EmployeeAccount>();
            CreateMap<AppointmentSaveDto, Appointment>();

            CreateMap<ExaminationSaveDto, Examination>()
                .ForMember(exam => exam.PrescriptionDetails, opt => opt.Ignore())
                .ForMember(exam => exam.Invoice, opt => opt.Ignore());

            CreateMap<PrescriptionDetailSaveDto, PrescriptionDetail>()
                .ForMember(pre => pre.ExaminationId, opt => opt.Ignore())
                .ForMember(pre => pre.Examination, opt => opt.Ignore());
           


        }
    }
}
