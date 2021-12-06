using Business.Contract.Services;
using Business.Services;
using Data.Contract.UnitOfWork;
using Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using UserIdentity.Data;

namespace designing_chart_api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddTransient<IExerciseUnitOfWork, ExerciseUnitOfWork>();
            service.AddTransient<IAuthentificationUnitOfWork, AuthentificationUnitOfWork>();
            service.AddTransient<IProfileDataService, ProfileDataService>();
            service.AddTransient<IProfileManager, ProfileManager<ApplicationUser>>();
            service.AddTransient<IAttemptService, AttemptService>();
            service.AddTransient<IExerciseService, ExerciseService>();
            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<IStatisticService, StatisticService>();
            return service;
        }
    }
}
