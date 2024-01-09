using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;
using JavaFlorist.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JavaFlorist.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
  

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> Wishlist(int id)
        {
            var data = await _wishlistService.GetList(id);
            return View(data);
        }


        public IActionResult Add(int cust_id,int bouquet_id)
        {
            Wishlist model =  new Wishlist();
            model.cust_id = cust_id;
            model.bouquet_id = bouquet_id;
            if (!ModelState.IsValid)
                return View(model);

            var result = _wishlistService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                //string url = $"/Wishlist/Wishlist?id={cust_id}";
                return Redirect(Request.Headers["referer"].ToString());
            }
            else
            {
                TempData["msg"] = "Error on server side";
                string url = $"/Wishlist/Wishlist?id={cust_id}";
                return Redirect(url);
            }
        }

		public IActionResult Delete(int id)
		{
			var result = _wishlistService.Delete(id);
            string returnUrl = Request.Headers["Referer"].ToString();
            return Redirect(returnUrl);
        }

	}
}
