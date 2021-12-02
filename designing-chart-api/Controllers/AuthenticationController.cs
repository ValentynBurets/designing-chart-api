using System;
using System.Threading.Tasks;
using AutoMapper;
using Business.Contract.Services;
using designing_chart_api.Models;
using designing_chart_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Data;

namespace designing_chart_api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IProfileRegistrationService _profileRegistrationService;

        public AuthenticationController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IAuthManager authManager,
            IProfileRegistrationService profileRegistrationService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
            _profileRegistrationService = profileRegistrationService;
        }

        [NonAction]
        private async Task<IActionResult> RegisterUser(RegisterUserModel userModel, string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApplicationUser>(userModel);
                user.UserName = userModel.Email;

                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                await _userManager.AddToRoleAsync(user, role);

                await _profileRegistrationService.CreateProfile(user, userModel.FirstName, userModel.LastName);

                if (!await _authManager.ValidateUser(userModel))
                {
                    return Unauthorized();
                }

                return Accepted(new
                {
                    Token = await _authManager.CreateToken()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterUserModel userModel)
        {
            return await RegisterUser(userModel, "Student");
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserModel userModel)
        {
            return await RegisterUser(userModel, "Admin");
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authManager.ValidateUser(userModel))
                {
                    return Unauthorized();
                }

                return Accepted(new
                {
                    Token = await _authManager.CreateToken()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
