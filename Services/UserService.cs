using Microsoft.AspNetCore.Identity;
using subscription_system.Data;
using subscription_system.Models;
using System.Security.Claims;

namespace subscription_system.Services {
    public class UserService: IUserService {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<List<User>> GetAllUser() { 
            return _userManager.Users.ToList();    
        }

        public async Task<IdentityResult> CreateUser(User user, string password) { 
            return await _userManager.CreateAsync(user, password);
            }

        public async Task<IdentityResult> UpdateUser(User user) {
            return await _userManager.UpdateAsync(user);    
        }

        public async Task<IdentityResult> DeleteUser(string userId) { 
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado"});
            return await _userManager.DeleteAsync(user);
            }

        public async Task<IdentityResult> AssignRole(string userId, string role) {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)return IdentityResult.Failed(new IdentityError{Description="Usuario no encontrado"});

            if(!await _roleManager.RoleExistsAsync(role)) { 
                await _roleManager.CreateAsync(new IdentityRole(role));
                }
            return await _userManager.AddToRoleAsync(user, role);
            }

        public async Task<IList<string>> GetUserRoles(string userId) {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null ? await _userManager.GetRolesAsync(user): new List<string>();
            }

        public async Task<IdentityResult> RemoveRole(string userId, string role) {
            var user= await _userManager.FindByIdAsync(userId);
            if(user == null ) return IdentityResult.Failed(new IdentityError { Description ="Usiario no encontrado"});
            return await _userManager.RemoveFromRoleAsync(user,role);
            }

        public async Task<IdentityResult> AddClaimToUser(string userId, string claimType, string claimValue) {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado" });

            return await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
        }
    }
}
