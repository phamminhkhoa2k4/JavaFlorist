using JavaFlorist.Models.DTO;
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

        [Area("Admin")]
        public async Task<IActionResult> Delete(string userId)
        {
            var isDeleted = await authService.DeleteUser(userId);
            if (isDeleted)
            {
                return RedirectToAction(nameof(AdminList));
            }

            return View("Error");
        }

        [Area("Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            model.Role = "Admin";

            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.ProfilePicture = imageName;
            }

            var result = await authService.AddUserAdmin(model);
            if (result.StatusCode == 1)
            {
                TempData["msg"] = result.Message;
                return View();
            }
            else if (result.StatusCode == 2)
            {
                TempData["msg"] = result.Message;
                return View();
            }
            //TempData["msg"] = result.Message;
            //TempData["RegisteredSuccessfully"] = true;
            return Redirect(Request.Headers["referer"].ToString());
        }
    }
}
