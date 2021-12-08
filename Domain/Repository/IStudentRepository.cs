using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IStudentRepository : IEntityRepository<Student>
    {
        Task<bool> Contains(Guid id);
        Task<bool> Contains(Student id);
    }
}
