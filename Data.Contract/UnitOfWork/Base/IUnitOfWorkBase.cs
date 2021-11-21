using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contract.UnitOfWork.Base
{
    public interface IUnitOfWorkBase
    {
        Task<int> Save();
    }
}
