using Domain.Entity;
using Domain.Entity.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IExerciseRepository : IEntityRepository<Exercise>
    {
        Task<Exercise> GetByTitle(string title);
        Task<IEnumerable<Exercise>> GetByStatus(StatusType status);
        Task<IEnumerable<Exercise>> GetByCategory(string category);
        Task<IEnumerable<Exercise>> GetByName();
        Task<IEnumerable<Exercise>> GetByNameDesc();
        Task<IEnumerable<Exercise>> GetByDate();
        Task<IEnumerable<Exercise>> GetByDateDesc();

    }
}
