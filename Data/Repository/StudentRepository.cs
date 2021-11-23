using Data.EF;
using Data.Repository.Base;
using Domain.Entity;
using Domain.Repository;
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
    }
}
