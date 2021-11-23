using AutoMapper;
using Business.Contract.Model;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }

    }
}
