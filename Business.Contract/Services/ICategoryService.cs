using Business.Contract.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contract.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<GetCategoryViewModel>> GetAll();
        public Task Create(string name);
    }
}
