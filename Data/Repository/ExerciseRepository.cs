using Data.EF;
using Data.Repository.Base;
using Domain.Entity;
using Domain.Entity.Constants;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ExerciseRepository : EntityRepositoryClass<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(DomainDbContext domainDbContext) : base(domainDbContext)
        {
        }

        public async Task<IEnumerable<Exercise>> GetByCategory(string category)
        {
            return await _DbContext.Exercises.Where(e => e.CategoryType.Name == category).ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> GetByStatus(StatusType status)
        {
            return await _DbContext.Exercises.Where(e => e.StatusType == status).ToListAsync();
        }

        public async Task<Exercise> GetByTitle(string title)
        {
            return await _DbContext.Exercises.FirstOrDefaultAsync(e => e.Title == title);
        }
    }
}
