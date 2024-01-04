using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JavaFlorist.Areas.Admin.Controllers
{
        [Authorize(Roles = "Admin")]
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        private readonly IFileService _fileService;
        public UserAuthenticationController(IUserAuthenticationService authService, IFileService fileService)
        {
            this.authService = authService;
            _fileService = fileService;
        }
        [Area("Admin")]
        public async Task<IActionResult> UserList()
        {
            var usersInRole = await authService.GetAllUsersInRole("User");
            return View(usersInRole);
        }
        [Area("Admin")]

        public async Task<IActionResult> AdminList()
        {
            var admins = await authService.GetAllUsersInRole("Admin");
            return View(admins);
        }

        public async Task<IActionResult> Delete(string userId)
        {
            var isDeleted = await authService.DeleteUser(userId);
            if (isDeleted)
            {
                return RedirectToAction(nameof(AdminList));
            }

            return View("Error");
        }

    }
}
