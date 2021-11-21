using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IAttemptRepository : IEntityRepository<Attempt>
    {
        Task<IEnumerable<Attempt>> GetByStudentId(Guid studentId);
        Task<Attempt> GetByExerciseId(Guid exerciseId);
    }
}
