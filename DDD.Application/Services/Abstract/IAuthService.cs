using DDD.Application.Dtos.AuthDtos;
using DDD.Application.ViewModels.RoleVM;

namespace DDD.Application.Services.Abstract
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDto login);
        Task<bool> RegisterAsync(RegisterDto model);
        Task<bool> AdminRegisterAsync(RegisterDto model);
        Task<bool> ConfirmMailAsync(ConfirmCodeDto model);
        Task<List<RoleAssignVM>> GetRoleAssingAsync(string id);
        Task<bool> RoleAssignAsync(List<RoleAssignVM> modelList, string id);
        Task<bool> LogoutAsync();
    }
}
