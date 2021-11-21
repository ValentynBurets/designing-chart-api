using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IExerciseRepository : IEntityRepository<Exercise>
    {
        Task<IEnumerable<Exercise>> GetByTitle(Guid title);
        Task<IEnumerable<Exercise>> GetByStudent(Guid studentId);
        Task<IEnumerable<Exercise>> GetByStatus(string status);
        Task<IEnumerable<Exercise>> GetByCategory(string category);
    }
}
