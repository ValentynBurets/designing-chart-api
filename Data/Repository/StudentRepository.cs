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
    public class StudentRepository: EntityRepositoryClass<Student>, IStudentRepository
    {
        public StudentRepository(DomainDbContext exerciseDbContext) : base(exerciseDbContext)
        {
        }

        public async Task<bool> Contains(Guid id)
        {
            var o = await _DbContext.Studens.FirstOrDefaultAsync(x => x.Id == id);
            if (o !=null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Contains(Student student)
        {
            return await _DbContext.Studens.ContainsAsync(student);
        }
    }
}
