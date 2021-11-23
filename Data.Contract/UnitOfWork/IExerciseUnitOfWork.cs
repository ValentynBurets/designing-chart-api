using Data.Contract.UnitOfWork.Base;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contract.UnitOfWork
{
    public interface IExerciseUnitOfWork : IUnitOfWorkBase
    {
        IAttemptRepository AttemptRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IExerciseRepository ExerciseRepository { get; }
        IStudentRepository StudentRepository { get; }
    }
}
