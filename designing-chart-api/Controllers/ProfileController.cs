using Business.Contract.Model;
using Business.Contract.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace designing_chart_api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {
        private readonly IProfileDataService _profileDataService;
        private readonly IProfileManager _profileManager;

        public ProfileController(IProfileDataService profileDataService, IProfileManager profileManager)
        {
            _profileDataService = profileDataService;
            _profileManager = profileManager;
        }

        #region Info data

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            List<UserInfoViewModel> usersInfoList = null;

            try
            {
                /*
                 */
                if (User.IsInRole("Admin"))
                {
                    usersInfoList = (List<UserInfoViewModel>)await _profileDataService.GetAllUsersInfo();
                }
                else
                {
                    throw new Exception("Invalid user role!");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok(usersInfoList);
        }

        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetMyInfo()
        {
            ProfileInfoModel profileInfo = null;

            try
            {
                if (User.IsInRole("Student"))
                {
                    profileInfo = await _profileDataService.GetStudentProfileInfoById(GetUserId());
                }
                else if (User.IsInRole("Admin"))
                {
                    profileInfo = await _profileDataService.GetAdminProfileInfoById(GetUserId());
                }
                else
                {
                    throw new Exception("Invalid user role!");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok(profileInfo);
        }

        [HttpPut]
        [Route("info/update")]
        public async Task<IActionResult> UpdateMyInfo([FromBody] ProfileInfoModel userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model validation error!");
            }

            try
            {
                if (User.IsInRole("Student"))
                {
                    await _profileDataService.UpdateStudentProfileInfoById(userInfo, GetUserId());
                }
                else if (User.IsInRole("Admin"))
                {
                    await _profileDataService.UpdateAdminProfileInfoById(userInfo, GetUserId());
                }
                else
                {
                    throw new Exception("Invalid user role!");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok();
        }

        #endregion

        #region Auth data


        #region Email

        [HttpGet]
        [Route("email")]
        public async Task<IActionResult> GetMyEmail()
        {
            string email = null;

            try
            {
                email = await _profileManager.GetEmailByUserId(GetUserId());
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok(email);
        }

        [HttpPut]
        [Route("email/update")]
        [Authorize]
        public async Task<IActionResult> UpdateMyEmail([FromBody] UpdateEmailModel emailModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model validation error!");
            }

            try
            {
                await _profileManager.UpdateEmailByUserId(emailModel, GetUserId());
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok();
        }

        #endregion

        #region Password

        [HttpPut]
        [Route("password/update")]
        public async Task<IActionResult> UpdateMyPassword([FromBody] UpdatePasswordModel passwordModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model validation error!");
            }

            try
            {
                await _profileManager.UpdatePasswordByUserId(passwordModel, GetUserId());
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok();
        }

        #endregion

        #endregion
    }
}
