using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;

namespace JavaFlorist.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {

        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);


        Task<Status>  Update(UserCustomerModel  model);

        Task<List<string>> GetUserByName(string name);

        Task<IEnumerable<ApplicationUser>> GetAllUsersInRole(string role);

        Task<bool> DeleteUser(string userId);

        //Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}
