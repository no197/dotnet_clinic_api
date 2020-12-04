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

            //Create<DTO, Entity>
            CreateMap<PatientDto, Patient>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<MedicineDto, Medicine>();
            CreateMap<RegisterDto, EmployeeAccount>();
        }
    }
}
