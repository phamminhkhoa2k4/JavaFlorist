using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace JavaFlorist.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationService authService;
        private readonly IFileService _fileService;
        public UserAuthenticationController(IUserAuthenticationService authService, IFileService fileService)
        {
            this.authService = authService;
            _fileService = fileService;
        }


        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            model.Role = "Admin";

            var result = await authService.RegisterAsync(model);
            if(result.StatusCode == 1)
            {
                TempData["msg"] = result.Message;
                return View();
            }else if (result.StatusCode == 2)
            {
                TempData["msg"] = result.Message;
                return View();
            }
            TempData["msg"] = result.Message;
            TempData["RegisteredSuccessfully"] = true;
            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Could not logged in..";
                return RedirectToAction(nameof(Login));
            }
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserCustomerModel model)
        {
     

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
            var result = await authService.Update(model);
            if(result.StatusCode == 1)
            {
                TempData["power"] = true;
                TempData["msg"] = result.Message;
                return Redirect("/Home/Index");
            }
            else if (result.StatusCode == 2)
            {
                TempData["power"] = true;
                TempData["msg"] = result.Message;
                return Redirect("/Home/Index");

            }
            TempData["power"] = true;
            TempData["msg"] = result.Message;
            return Redirect("/Home/Index");

        }





    }
}

