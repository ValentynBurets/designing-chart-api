using Data.Contract.UnitOfWork.Base;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contract.UnitOfWork
{
    public interface IAuthentificationUnitOfWork : IUnitOfWorkBase
    {
        IStudentRepository StudentRepository { get; }
        IAdminRepository AdminRepository { get; }

    }
}
