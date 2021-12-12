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
using Newtonsoft.Json;

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

        public async Task Create(CreateAttemptViewModel new_attempt, Guid id)
        {
            //if (await _unitOfWork.AttemptRepository.GetByChart(new_attempt.Chart) != null)
            //{
            //    throw new ValidationException("Attempt with this chart exists");
            //}

            var existed_exercise = await _unitOfWork.ExerciseRepository.GetById(new_attempt.ExerciseId);
            if(existed_exercise == null)
            {
                throw new ValidationException($"Exercise with Id{new_attempt.ExerciseId} don`t exists");
            }
             
            //if (await _unitOfWork.StudentRepository.Contains(new_attempt.StudentId) == false)
            //{
            //    throw new ValidationException("Student with this Id don`t exists");
            //}

            var attempt = _mapper.Map<CreateAttemptViewModel, Attempt>(new_attempt);
            attempt.StudentId = (await _unitOfWork.StudentRepository.FirstOrDefault(x=>x.IdLink == id)).Id;

            var expiration_date = await _unitOfWork.ExerciseRepository.GetExpirationDateByExerciseId(attempt.ExerciseId);

            //if(DateTime.Compare(expiration_date, new_attempt.StartTime) < 0)
            //{
            //    throw new ValidationException("The student started this exercise too late. solving started later than expiration time allowed.");
            //}
            var percent = CalculateSimilarity(Simplify(attempt.Chart), Simplify(existed_exercise.EtalonChart));
            // percent = CalculateSimilarity(attempt.Chart, existed_exercise.EtalonChart);
            attempt.Mark = existed_exercise.MaxMark * percent;

            await _unitOfWork.AttemptRepository.Add(attempt);

            await _unitOfWork.Save();
        }

        public async Task<GetAttemptViewModel> GetById(Guid Id)
        {
            var attempt = await _unitOfWork.AttemptRepository.GetById(Id);

            if (attempt == null)
                throw new ValidationException("Can't find attempt. Operation canceled");

            
            //DeserializeObject
            dynamic student_chart  = JObject.Parse(attempt.Chart);
            dynamic etalon_chart = JObject.Parse(attempt.Exercise.EtalonChart);

            //calculate mark

            var percent = CalculateSimilarity(Simplify(student_chart), Simplify(etalon_chart));

            attempt.Mark = attempt.Exercise.MaxMark * percent / 100;

            var attemptViewModels = _mapper.Map<Attempt, GetAttemptViewModel>(attempt);
            attemptViewModels.PerCent = percent;

            return attemptViewModels;
        }

        private string Simplify(string chart)
        {
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(chart);
            StringBuilder stringify = new StringBuilder();
            var connections = new object[obj.connectors.Count];
            for (var i = 0; i < obj.connectors.Count; i++)
            {
                (string, string) sourceID = ("", "");
                (string, string) targetID = ("", "");
                for (var j = 0; j < obj.nodes.Count; j++)
                {
                    if (obj.nodes[j].id == obj.connectors[i].sourceID)
                    {
                        sourceID = (obj.nodes[j].shape.type, obj.nodes[j].shape.shape);
                    }
                    if (obj.nodes[j].id == obj.connectors[i].targetID)
                    {
                        targetID = (obj.nodes[j].shape.type, obj.nodes[j].shape.shape);
                    }
                }
                var feed = new
                {
                    type = obj.connectors[i].type,
                    sourceID = sourceID.Item1 + sourceID.Item2,
                    targetID = targetID.Item1 + targetID.Item2
                };
                connections[i] = feed;
                stringify.Append(
                    $"[type:{feed.type.ToString()},sourceID:{feed.sourceID.ToString()},targetID:{feed.targetID.ToString()}]");
            }

            return stringify.ToString();
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
