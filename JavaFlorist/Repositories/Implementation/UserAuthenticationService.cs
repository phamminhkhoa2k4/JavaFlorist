using Microsoft.AspNetCore.Identity;
using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using System.Security.Claims;
using JavaFlorist.Migrations;
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
            status.StatusCode = 0; // Hoặc giá trị khác để chỉ ra thành công
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



        //public async task<status> changepasswordasync(changepasswordmodel model, string username)
        //{
        //    var status = new status();

        //    var user = await usermanager.findbynameasync(username);
        //    if (user == null)
        //    {
        //        status.message = "user does not exist";
        //        status.statuscode = 0;
        //        return status;
        //    }
        //    var result = await usermanager.changepasswordasync(user, model.currentpassword, model.newpassword);
        //    if (result.succeeded)
        //    {
        //        status.message = "password has updated successfully";
        //        status.statuscode = 1;
        //    }
        //    else
        //    {
        //        status.message = "some error occcured";
        //        status.statuscode = 0;
        //    }
        //    return status;

        //}
    }
}
