﻿using Business.Contract.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contract.Services
{
    public interface IProfileDataService
    {
        public Task<UserInfoViewModel> GetStudentProfileInfoById(Guid id);
        public Task<IEnumerable<UserInfoViewModel>> GetAllUsersInfo();
        public Task<UserInfoViewModel> GetAdminProfileInfoById(Guid id);
        public Task UpdateStudentProfileInfoById(ProfileInfoModel model, Guid id);
        public Task UpdateAdminProfileInfoById(ProfileInfoModel model, Guid id);
    }
}
