using  subscription_system.Models;
using Microsoft.AspNetCore.Identity;
namespace subscription_system.Services {
    public interface IUserService {
        Task<List<User>> GetAllUser();

        Task<IdentityResult> CreateUser( User user, string password);

        Task<IdentityResult> UpdateUser(User user);

        Task<IdentityResult> DeleteUser(string userId);

        Task<IdentityResult> AssignRole(string userId, string role);

        Task<IList<string>> GetUserRoles(string userId);

        Task<IdentityResult> RemoveRole(string userId, string role);
        Task<IdentityResult> AddClaimToUser(string userId, string claimType, string claimValue);
    }
}
