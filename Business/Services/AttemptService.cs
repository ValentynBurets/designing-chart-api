using AutoMapper;
using Business.Contract.Model;
using Business.Contract.Services;
using Data.Contract.UnitOfWork;
using Domain.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AttemptService : IAttemptService
    {
        private IMapper _mapper;
        private readonly IExerciseUnitOfWork _unitOfWork;

        public AttemptService(IMapper mapper, IExerciseUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CreateAttemptViewModel new_attempt)
        {
            if (await _unitOfWork.AttemptRepository.GetByChart(new_attempt.Chart) != null)
            {
                throw new ValidationException("Attempt with this chart exists");
            }

            var existed_exercise = await _unitOfWork.ExerciseRepository.GetById(new_attempt.ExerciseId);
            if(existed_exercise == null)
            {
                throw new ValidationException($"Exercise with Id{new_attempt.ExerciseId} don`t exists");
            }

            var existed_student = await _unitOfWork.StudentRepository.GetById(new_attempt.StudentId);
            if (existed_student == null)
            {
                throw new ValidationException($"Student with Id{new_attempt.StudentId} don`t exists");
            }

            var attempt = _mapper.Map<CreateAttemptViewModel, Attempt>(new_attempt);

            await _unitOfWork.AttemptRepository.Add(attempt);

            await _unitOfWork.Save();
        }

        public async Task<GetAttemptViewModel> GetById(Guid Id)
        {
            var attempt = await _unitOfWork.AttemptRepository.GetById(Id);

            if (attempt == null)
                throw new ValidationException($"Can't find attempt. Operation canceled");

            
            //DeserializeObject
            dynamic student_chart  = JObject.Parse(attempt.Chart);
            dynamic etalon_chart = JObject.Parse(attempt.Exercise.EtalonChart);

            //calculate mark

            var percent = CalculateSimilarity(student_chart, etalon_chart);

            attempt.Mark = attempt.Exercise.MaxMark * percent / 100;

            var attemptViewModels = _mapper.Map<Attempt, GetAttemptViewModel>(attempt);
            attemptViewModels.PerCent = percent;

            return attemptViewModels;
        }

        private int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        private double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

    }
}
