using Data.Contract.UnitOfWork;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserIdentity.Data;
using System.Threading.Tasks;
using Business.Contract.Services;

namespace Business.Services
{
    public class ProfileRegistrationService : IProfileRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IAuthentificationUnitOfWork _unitOfWork;
        public ProfileRegistrationService(UserManager<ApplicationUser> userManager,
            IAuthentificationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<bool> CreateProfile(ApplicationUser user, string firstName, string lastName)
        {
            IList<string> role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Student"))
            {
                await _unitOfWork.StudentRepository.Add(new Student(Guid.Parse(user.Id))
                {
                    Name = firstName,
                    SurName = lastName
                });
                await _unitOfWork.Save();
                return true;
            }
            else if (role.Contains("Admin"))
            {
                await _unitOfWork.AdminRepository.Add(new Admin(Guid.Parse(user.Id))
                {
                    Name = firstName,
                    SurName = lastName
                });
                await _unitOfWork.Save();
                return true;
            }
            else
            {
                throw new Exception("Role is not set");
            }
        }
    }
}
