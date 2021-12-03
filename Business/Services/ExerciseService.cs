using AutoMapper;
using Business.Contract.Model;
using Business.Contract.Services;
using Data.Contract.UnitOfWork;
using Domain.Entity;
using Domain.Entity.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(CreateExerciseViewModel new_exercise)
        {
            if(new_exercise == null)
            {
                throw new ValidationException("Data don't exists");
            }
            if (new_exercise.EtalonChart == null)
            {
                throw new ValidationException("Etalon chart don't exists");
            }

            var exercise = _mapper.Map<CreateExerciseViewModel, Exercise>(new_exercise);

            if (await _unitOfWork.ExerciseRepository.Contains(exercise))
            {
                throw new ValidationException("Exercise with this data exists");
            }

            if (await _unitOfWork.ExerciseRepository.Contains(new_exercise.Title))
            {
                throw new ValidationException("Exercise with this title exists");
            }

            var existed_category = await _unitOfWork.CategoryRepository.GetByCategoryName(new_exercise.Category);
            
            if(new_exercise.Category == null)
            {
                throw new ValidationException("Category is null");
            }

            if (existed_category == null)
            {
                await _unitOfWork.CategoryRepository.Add(new CategoryType() { Name = new_exercise.Category });
                existed_category = await _unitOfWork.CategoryRepository.GetByCategoryName(new_exercise.Category);
            }

            if(new_exercise.EtalonChart == null)
            {
                throw new ValidationException("Etalon Chart is null");
            }

            exercise.CategoryId = existed_category.Id;
            exercise.StatusType = StatusType.Active;

            await _unitOfWork.ExerciseRepository.Add(exercise);

            await _unitOfWork.Save();
        }

        public async Task Delete(Guid Id)
        {
            var exercise = await _unitOfWork.ExerciseRepository.GetById(Id);

            if(exercise == null)
                throw new ValidationException($"Can't find exercise with Id ({Id}). Operation canceled");

            await _unitOfWork.ExerciseRepository.Remove(exercise);
            await _unitOfWork.Save();
        }

        public async Task<Exercise> Edit(Guid Id, CreateExerciseViewModel exercise)
        {
            var existed_exercise = await _unitOfWork.ExerciseRepository.GetById(Id);

            if (existed_exercise == null)
            {
                throw new ValidationException($"Exercise with id { Id } isn`t  exist");
            }

            var existed_category = await _unitOfWork.CategoryRepository.GetByCategoryName(exercise.Category);

            if(existed_category == null)
            {
                await _unitOfWork.CategoryRepository.Add(new CategoryType() { Name = exercise.Category });
                existed_category = await _unitOfWork.CategoryRepository.GetByCategoryName(exercise.Category);
            }

            existed_exercise.EtalonChart = exercise.EtalonChart;
            existed_exercise.CategoryId = existed_category.Id;
            existed_exercise.ExpirationDate = exercise.ExpirationDate;
            existed_exercise.MaxMark = exercise.MaxMark;
            existed_exercise.Title = exercise.Title;

            await _unitOfWork.ExerciseRepository.Update(existed_exercise);
            await _unitOfWork.Save();

            return await _unitOfWork.ExerciseRepository.GetByTitle(exercise.Title);
        }

        public async Task<IEnumerable<GetExerciseViewModel>> GetAll()
        {
            var exercises = await _unitOfWork.ExerciseRepository.GetAll();

            if(exercises == null)
                throw new ValidationException($"Can't find exercises. Operation canceled");

            var exerciseViewModels = _mapper.Map<IEnumerable<Exercise>, IEnumerable<GetExerciseViewModel>>(exercises);
            
            return exerciseViewModels;
        }

        public async Task<IEnumerable<GetExerciseViewModel>> GetSorted(string sortOrder)
        {
            List<Exercise> exercises = new List<Exercise>();

            switch (sortOrder)
            {
                case "name":
                    exercises = (List<Exercise>)await _unitOfWork.ExerciseRepository.GetByName();
                    break;
                case "name_desc":
                    exercises = (List<Exercise>)await _unitOfWork.ExerciseRepository.GetByNameDesc();
                    break;
                case "date":
                    exercises = (List<Exercise>)await _unitOfWork.ExerciseRepository.GetByDate();
                    break;
                case "date_desc":
                    exercises = (List<Exercise>)await _unitOfWork.ExerciseRepository.GetByDateDesc();
                    break;
                default:
                    exercises = (List<Exercise>)await _unitOfWork.ExerciseRepository.GetByName();
                    break;
            }

            if (exercises == null)
                throw new ValidationException($"Can't find exercises. Operation canceled");

            var exerciseViewModels = _mapper.Map<IEnumerable<Exercise>, IEnumerable<GetExerciseViewModel>>(exercises);

            return exerciseViewModels;
        }
    }
}
