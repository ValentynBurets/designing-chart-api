using AutoMapper;
using Business.Contract.Model;
using Business.Contract.Services;
using Data.Contract.UnitOfWork;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProfileDataService : IProfileDataService
    {
        private readonly IAuthentificationUnitOfWork _unitOfWork;
        private readonly IProfileManager _profileManager;
        private readonly IMapper _mapper;

        public ProfileDataService(IAuthentificationUnitOfWork unitOfWork, IMapper mapper, IProfileManager profileManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _profileManager = profileManager;
        }

        public async Task<ProfileInfoModel> GetStudentProfileInfoById(Guid id)
        {
            var student = await _unitOfWork.StudentRepository.GetById(id);

            if (student == null)
            {
                throw new Exception("Employee with this id was not found!");
            }

            var profileInfo = _mapper.Map<Student, ProfileInfoModel>(student);

            return profileInfo;
        }

        public async Task<ProfileInfoModel> GetAdminProfileInfoById(Guid id)
        {
            var admin = await _unitOfWork.AdminRepository.GetById(id);

            if (admin == null)
            {
                throw new Exception("Admin with this id was not found!");
            }

            var profileInfo = _mapper.Map<Admin, ProfileInfoModel>(admin);

            return profileInfo;
        }

      

        public async Task UpdateStudentProfileInfoById(ProfileInfoModel model, Guid id)
        {
            var student = await _unitOfWork.StudentRepository.GetById(id);

            if (student == null)
            {
                throw new Exception("Employee with this id was not found!");
            }

            student.Name = model.Name;
            student.SurName = model.SurName;

            await _unitOfWork.Save();
        }

        public async Task UpdateAdminProfileInfoById(ProfileInfoModel model, Guid id)
        {
            var admin = await _unitOfWork.AdminRepository.GetById(id);

            if (admin == null)
            {
                throw new Exception("Admin with this id was not found!");
            }

            admin.Name = model.Name;
            admin.SurName = model.SurName;

            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<UserInfoViewModel>> GetAllUsersInfo()
        {
            List<UserInfoViewModel> userList = new List<UserInfoViewModel>();

            var students = (await _unitOfWork.StudentRepository.GetAll()).ToList();

            if (students == null)
            {
                throw new Exception("Students not found!");
            }

            foreach (var item in students)
            {
                var email = await _profileManager.GetEmailByUserId(item.IdLink);
                var user = _mapper.Map<Student, UserInfoViewModel>(item);
                user.Email = email;
                userList.Add(user);
            }

            var admins = (await _unitOfWork.AdminRepository.GetAll()).ToList();

            if (admins == null)
            {
                throw new Exception("Admins not found!");
            }

            foreach (var item in admins)
            {
                var email = await _profileManager.GetEmailByUserId(item.IdLink);
                var user = _mapper.Map<Admin, UserInfoViewModel>(item);
                user.Email = email;
                userList.Add(user);
            }

            return userList;
        }
    }
}
