using AutoMapper;
using Business.Contract.Model;
using designing_chart_api.Models;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentity.Data;

namespace designing_chart_api.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<CategoryType, GetCategoryViewModel>().ReverseMap();
            CreateMap<Exercise, GetExerciseViewModel>().ReverseMap();
            CreateMap<Exercise, CreateExerciseViewModel>().ReverseMap();
            CreateMap<Attempt, CreateAttemptViewModel>().ReverseMap();
            CreateMap<ApplicationUser, RegisterUserModel>().ReverseMap();

            CreateMap<Admin, UserInfoViewModel>()
                .ForMember("Role", opt => opt.MapFrom(a => "Admin"));
            CreateMap<Student, UserInfoViewModel>()
                .ForMember("Role", opt => opt.MapFrom(e => "Student"));
            CreateMap<Student, ProfileInfoModel>();

            CreateMap<Admin, ProfileInfoModel>();
        }

    }
}
