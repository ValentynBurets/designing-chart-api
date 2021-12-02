using designing_chart_api.Models;
using System.Threading.Tasks;

namespace designing_chart_api.Services
{
    public interface IAuthManager
    {
        Task<string> CreateToken();
        Task<bool> ValidateUser(LoginUserModel userModel);
    }
}