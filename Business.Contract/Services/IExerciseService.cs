using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Contract.Model;
using Domain.Entity;

namespace Business.Contract.Services
{
    public interface IExerciseService
    {
        public Task Create(CreateExerciseViewModel exercise);
        public Task<Exercise> Edit(Guid Id, CreateExerciseViewModel exercise);
        public Task<IEnumerable<GetExerciseViewModel>> GetAll();
        public Task<IEnumerable<GetExerciseViewModel>> GetSorted(string sortOrder);
        public Task Delete(Guid Id);
    }
}
