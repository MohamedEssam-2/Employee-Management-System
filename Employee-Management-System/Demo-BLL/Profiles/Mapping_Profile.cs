using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo_BLL.DTOs.EmployeeDto;
using Demo_DAL.Models.Employee;
using Microsoft.Data.SqlClient;

namespace Demo_BLL.Profiles
{
    public class Mapping_Profile : Profile
    {
        public Mapping_Profile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(des => des.EmpGender, option => option.MapFrom(src => src.Gender))
                .ForMember(des => des.EmployeeType, option => option.MapFrom(src => src.EmployeeType))
                .ForMember(des => des.Department, option => option.MapFrom(src => src.Department!=null ?src.Department.Name:null))     
                .ReverseMap();

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(des => des.HiringDate, option => option.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(des => des.Department, option => option.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ReverseMap();

            CreateMap<CreateEmployeeDto,Employee>()
                  .ForMember(des => des.HiringDate, option => option.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly())))
                .ReverseMap();

            CreateMap<UpdateEmployeeDto, Employee>()
                 .ForMember(des => des.HiringDate, option => option.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly()))).ReverseMap();
        }
    }
}
