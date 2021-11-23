using AutoMapper;
using Business.Contract.Model;
using Business.Contract.Services;
using Data.Contract.UnitOfWork;
using Domain.Entity;
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
        private Mapper _mapper;
        private readonly IExerciseUnitOfWork _unitOfWork;

        public AttemptService(Mapper mapper, IExerciseUnitOfWork unitOfWork)
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
    }
}
