using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JavaFlorist
{
    public class AdminAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Nếu người dùng chưa đăng nhập, chuyển hướng tới trang đăng nhập và hiển thị thông báo
                context.Result = new RedirectToActionResult("Login", "UserAuthentication", new { area = "" });
                return;
            }

            if (!context.HttpContext.User.IsInRole("Admin"))
            {
                // Nếu người dùng không có quyền admin, chuyển hướng tới trang đăng nhập và hiển thị thông báo
                context.Result = new RedirectToActionResult("Login", "UseAuthentication", new { area = "" });
                return;
            }

            base.OnActionExecuting(context);
        }
    }

}