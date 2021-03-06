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
            CreateMap<Exercise, GetExerciseViewModel>()
                .AfterMap((src, dest) =>
                {
                    dest.Status = src.StatusType.ToString();
                });
            CreateMap<Exercise, CreateExerciseViewModel>().ReverseMap();
            CreateMap<Attempt, CreateAttemptViewModel>().ReverseMap();
            CreateMap<Attempt, GetAttemptViewModel>()
                .AfterMap((src, dest, context) =>
                {
                    dest.ExerciseInfo = context.Mapper.Map<Exercise, GetExerciseViewModel>(src.Exercise);
                });
        }
    }
}