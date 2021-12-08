using AutoMapper;
using Business.Contract.Model;
using Business.Contract.Services;
using Data.Contract.UnitOfWork;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IMapper _mapper;
        private readonly IExerciseUnitOfWork _unitOfWork;

        public StatisticService(IMapper mapper, IExerciseUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        private async Task<double> GetCourseMaxMark() => (await _unitOfWork.ExerciseRepository.GetAll()).Sum(ex => ex.MaxMark);

        private async Task<IEnumerable<StatisticReportModel>> GetUserStatistics(Guid studentId, DateTime? startDate = null, DateTime? endDate = null, string category = null)
        {
            var attempts = (await _unitOfWork.AttemptRepository.GetAll())
                .Where(attempt => attempt.StudentId == studentId);

            double courseMaxMark = await GetCourseMaxMark();

            attempts = FilrerByCategory(category, attempts);
            
            attempts = FilterByDateRange(startDate, endDate, attempts);
            
            List<StatisticReportModel> staticticReports = new();
            
            var exerciseGroups = attempts
                .GroupBy(a => a.ExerciseId)
                .Select(
                    g => new
                    {
                        Name = g.Key,
                        Attempts = g.Select(p => p)
                    }
                );
            
            foreach (var g in exerciseGroups)
            {
                staticticReports.Add(
                    new StatisticReportModel
                    {
                        Attempts = _mapper.Map<IEnumerable<Attempt>, IEnumerable<GetAttemptViewModel>>(g.Attempts)
                    }
                );
            }
            staticticReports.ToList().ForEach(s => s.CoursePercentage = s.MaxMark/ courseMaxMark * 100);

            return staticticReports;
        }

        private static IEnumerable<Attempt> FilterByDateRange(DateTime? startDate, DateTime? endDate, IEnumerable<Attempt> attempts)
        {
            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue && endDate.HasValue)
                {
                    attempts = attempts.Where(a => a.FinishTime >= startDate && a.FinishTime <= endDate);
                }
                else if (startDate.HasValue)
                {
                    attempts = attempts.Where(a => a.FinishTime >= startDate);
                }
                else
                {
                    attempts = attempts.Where(a => a.FinishTime <= endDate);
                }
            }

            return attempts;
        }

        private static IEnumerable<Attempt> FilrerByCategory(string category, IEnumerable<Attempt> attempts)
        {
            if (!string.IsNullOrEmpty(category))
            {
                attempts = attempts
                    .Where(a => a.Exercise.CategoryType.Name.Contains(category));
            }

            return attempts;
        }

        public async Task<IEnumerable<StatisticReportModel>> GetStatistics(Guid? studentId = null, DateTime? startDate = null, DateTime? endDate = null, string category = null)
        {
            return await GetStatistics(studentId.Value, startDate, endDate, category);
        }

        public async Task<IEnumerable<StatisticReportModel>> GetStatistics(string studentName = null, DateTime? startDate = null, DateTime? endDate = null, string category = null)
        {
            var result = new List<StatisticReportModel>();
            if (string.IsNullOrEmpty(studentName))
            {
                var students = await _unitOfWork.StudentRepository.GetAll();
                foreach (Student student in students)
                {

                    result.AddRange(await GetUserStatistics(student.Id, startDate, endDate, category));
                }
            }
            else 
            {
                var studentId = (await _unitOfWork.StudentRepository.FirstOrDefault(stud => stud.Name.Contains(studentName) || stud.SurName.Contains(studentName))).Id;
                result.AddRange(await GetUserStatistics(studentId, startDate, endDate, category));
            }
            return result;
        }
    }
}
