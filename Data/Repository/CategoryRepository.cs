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
    public class CategoryRepository : EntityRepositoryClass<CategoryType>, ICategoryRepository
    {
        public CategoryRepository(DomainDbContext domainDbContext): base(domainDbContext)
        {
        }

        public async Task<CategoryType> GetByCategoryName(string categoryName)
        {
            return await _DbContext.CategoryTypes.FirstOrDefaultAsync(c => c.Name == categoryName);
        }
    }
}
