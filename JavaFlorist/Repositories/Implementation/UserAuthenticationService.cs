using Microsoft.AspNetCore.Identity;
using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace JavaFlorist.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DatabaseContext ctx;
        private readonly SignInManager<ApplicationUser> signInManager;
        public UserAuthenticationService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, DatabaseContext ctx)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.ctx = ctx;

        }

        public async Task<Status> RegisterAsync(RegistrationModel model)
        {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 1;
                status.Message = "User already exist";
                return status;
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Customer = new Customer
                {
                    F_name = "",
                    L_name = "",
                    Dob = DateTime.Now,
                    Gender = "",
                    P_no = 0,
                    Address = "",
                }

            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 2;
                status.Message = "User creation failed";
                return status;
            }

            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));


            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            // Đăng nhập người dùng mới đăng ký thành công
            await signInManager.SignInAsync(user, isPersistent: false);


            status.StatusCode = 0;
            status.Message = "You have registered successfully";
            return status;
        }


        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),

                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully";
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is locked out";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on logging in";
            }

            return status;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();

        }



        public async Task<Status> Update(UserCustomerModel model)
        {
            var status = new Status();



            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                status.StatusCode = 1;
                return status;
            }

            // Cập nhật thông tin cho ApplicationUser
            user.UserName = model.Username;
            user.Email = model.Email;
            user.ProfilePicture = model.ProfilePicture;
            if (user.Customer == null)
            {
                user.Customer = new Customer(); // Assuming Customer is a class or entity
            }
            // Cập nhật thông tin Customer
            user.Customer.F_name = model.F_name;
            user.Customer.L_name = model.L_name;
            user.Customer.Dob = model.Dob;
            user.Customer.Gender = model.Gender;
            user.Customer.P_no = model.P_no;
            user.Customer.Address = model.Address;


            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // Xử lý khi cập nhật không thành công
                status.StatusCode = 2; // Đánh dấu cập nhật không thành công
                return status;
            }
            status.StatusCode = 0; 
            status.Message = "Updated Successfull ! =))";

            return status;
        }

        public async Task<List<string>> GetUserByName(string name)
        {
            var user = await userManager.FindByNameAsync(name) ;

            if (user == null)
            {
                // Xử lý khi không tìm thấy người dùng
                return new List<string>();
            }

            var roles = await userManager.GetRolesAsync(user);

            return roles.ToList();
        }


        public async Task<IEnumerable<ApplicationUser>> GetAllUsersInRole(string role)
        {
            var usersInRole = await userManager.GetUsersInRoleAsync(role);
            return usersInRole;
        }

 

        public async Task<bool> DeleteUser(string userId)
        {
            var userToDelete = await userManager.FindByIdAsync(userId);
            if (userToDelete == null)
            {
                return false;
            }

            var result = await userManager.DeleteAsync(userToDelete);
            return result.Succeeded;
        }

        public async Task<Status> AddUserAdmin(RegistrationModel model)
        {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 1;
                status.Message = "User already exist";
                return status;
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                ProfilePicture = model.ProfilePicture,
                Customer = new Customer
                {
                    F_name = "",
                    L_name = "",
                    Dob = DateTime.Now,
                    Gender = "",
                    P_no = 0,
                    Address = "",
                }

            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 2;
                status.Message = "User creation failed";
                return status;
            }

            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));


            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            // Đăng nhập người dùng mới đăng ký thành công
            //await signInManager.SignInAsync(user, isPersistent: false);


            status.StatusCode = 0;
            status.Message = "You have registered successfully";
            return status;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await userManager.Users.ToListAsync();
        }
        public async Task<Status> ChangePasswordAsync(ChangePasswordModel model)
        {
            var status = new Status();

            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                status.Message = "user does not exist";
                status.StatusCode = 0;
                return status;
            }
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                status.Message = "password has updated successfully";
                status.StatusCode = 1;
            }
            else
            {
                status.Message = "some error occcured";
                status.StatusCode = 0;
            }
            return status;

        }
    }
}
