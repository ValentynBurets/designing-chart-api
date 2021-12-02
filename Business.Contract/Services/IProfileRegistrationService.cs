using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Data;

namespace Business.Contract.Services
{
    public interface IProfileRegistrationService
    {
        public Task<bool> CreateProfile(ApplicationUser user, string firstName, string lastName);
    }
}
