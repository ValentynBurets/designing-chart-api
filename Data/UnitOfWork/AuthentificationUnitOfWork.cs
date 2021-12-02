using Data.Contract.UnitOfWork;
using Data.EF;
using Data.Repository;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
    public class AuthentificationUnitOfWork : IAuthentificationUnitOfWork
    {
        private DomainDbContext _domainDbContext;
        private IStudentRepository _studentRepository;
        private IAdminRepository _adminRepository;

        public AuthentificationUnitOfWork(DomainDbContext domainDbContext)
        {
            _domainDbContext = domainDbContext;
        }
        public IStudentRepository StudentRepository => _studentRepository ??= new StudentRepository(_domainDbContext);

        public IAdminRepository AdminRepository => _adminRepository ??= new AdminRepository(_domainDbContext);

        public async Task<int> Save() => await _domainDbContext.SaveChangesAsync();
        
    }
}
