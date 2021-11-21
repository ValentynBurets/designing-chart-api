using Domain.Entity;
using Domain.Entity.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface ICategoryRepository : IEntityRepository<CategoryType>
    {
        Task<CategoryType> GetByCategoryName(string categoryName);
    }
}
