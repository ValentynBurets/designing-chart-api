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

        private async Task<IEnumerable<ExerciseResultReport>> GetExercisesStatistics(Guid studentId, DateTime? startDate = null, DateTime? endDate = null, string category = null)
        {
            var attempts = (await _unitOfWork.AttemptRepository.GetAll())
                .Where(attempt => attempt.StudentId == studentId);

            double courseMaxMark = await GetCourseMaxMark();

            attempts = FilrerByCategory(category, attempts);
            
            attempts = FilterByDateRange(startDate, endDate, attempts);
            
            var staticticReports = attempts
                .GroupBy(a => a.Exercise)
                .Select(
                    g => new ExerciseResultReport
                    {
                        Id = g.Key.Id,
                        Title = g.Key.Title,
                        Attempts = g.Select(p => _mapper.Map<Attempt, AttemptResultReport>(p))
                    }
                ).Select(x=>x).ToList();

            staticticReports.ForEach(s => s.CoursePercentage = s.MaxMark/ courseMaxMark * 100);

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

        private async Task<UserStatisticReport> GetUserStatistics(Guid? studentId = null, DateTime? startDate = null, DateTime? endDate = null, string category = null)
        {
            var student = (await _unitOfWork.StudentRepository.FirstOrDefault(x=>x.IdLink == studentId));
            var result = new UserStatisticReport
            {
                User = _mapper.Map<Student, ProfileInfoModel>(student)
            };
            result.Exercises = await GetExercisesStatistics(studentId.Value, startDate, endDate, category);
            return result;
        }

        public async Task<IEnumerable<UserStatisticReport>> GetStatistics(Guid? studentId = null, DateTime? startDate = null, DateTime? endDate = null, string category = null)
        {
            var userStatistics = await GetUserStatistics(studentId, startDate, endDate, category);
            var result = new List<UserStatisticReport> { userStatistics };
            return result;
        }

        public async Task<IEnumerable<UserStatisticReport>> GetStatistics(string studentName = null, DateTime? startDate = null, DateTime? endDate = null, string category = null)
        {
            var result = new List<UserStatisticReport>();
            if (string.IsNullOrEmpty(studentName))
            {
                var students = await _unitOfWork.StudentRepository.GetAll();
                foreach (Student student in students)
                {
                    var userReport = await GetUserStatistics(student.Id, startDate, endDate, category);
                    result.Add(userReport);
                }
            }
            else 
            {
                var student = (await _unitOfWork.StudentRepository.FirstOrDefault(stud => stud.Name.Contains(studentName) || stud.SurName.Contains(studentName)));
                var userReport = await GetUserStatistics(student.Id, startDate, endDate, category);
                result.Add(userReport);
            }
            return result;
        }
    }
}
