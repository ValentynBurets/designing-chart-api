using Data.Contract.UnitOfWork;
using Data.EF;
using Data.Repository;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
    public class ExerciseUnitOfWork : IExerciseUnitOfWork
    {
        private readonly DomainDbContext _dbContext;
        private IAttemptRepository _attemptRepository;
        private ICategoryRepository _categoryRepository;
        private IExerciseRepository _exerciseRepository;
        private IStudentRepository _studentRepository;

        public ExerciseUnitOfWork(DomainDbContext exercisesDbContext)
        {
            _dbContext = exercisesDbContext;
        }

        public IAttemptRepository AttemptRepository => 
            _attemptRepository ??= new AttemptRepository(_dbContext);

        public ICategoryRepository CategoryRepository => 
            _categoryRepository ??= new CategoryRepository(_dbContext);

        public IExerciseRepository ExerciseRepository => 
            _exerciseRepository ??= new ExerciseRepository(_dbContext);

        public IStudentRepository StudentRepository => 
            _studentRepository ??= new StudentRepository(_dbContext);

        public async Task<int> Save() => await _dbContext.SaveChangesAsync();
    }
}
