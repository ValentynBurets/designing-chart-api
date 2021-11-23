using Business.Contract.Model;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contract.Services
{
    public interface IAttemptService
    {
        public Task Create(CreateAttemptViewModel exercise);
        //public Task<Attempt> Edit(Guid Id, CreateAttemptViewModel exercise);
        //public Task<IEnumerable<GetAttemptViewModel>> GetAll();
        //public Task<Attempt> Delete(Guid Id);
    }
}
