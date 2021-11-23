using Data.EF;
using Data.Repository.Base;
using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AttemptRepository : EntityRepositoryClass<Attempt>, IAttemptRepository
    {
        public AttemptRepository(DomainDbContext domainDbContext): base(domainDbContext)
        { 
        }

        public async Task<IEnumerable<Attempt>> GetByExerciseId(Guid exerciseId)
        {
            return await _DbContext.Attempts.Where(e => e.ExerciseId == exerciseId).ToListAsync();
        }

        public async Task<IEnumerable<Attempt>> GetByStudentId(Guid studentId)
        {
            return await _DbContext.Attempts.Where(e => e.StudentId == studentId).ToListAsync();
        }
        
        public async Task<Attempt> GetByChart(string chart)
        {
            return await _DbContext.Attempts.FirstAsync(c => c.Chart == chart);
        }
    }
}
