using AutoMapper;
using Clinic.Dtos;
using Clinic.Models;
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

            //Create<DTO, Entity>
            CreateMap<PatientDto, Patient>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
