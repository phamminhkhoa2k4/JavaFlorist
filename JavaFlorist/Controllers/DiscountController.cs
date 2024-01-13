
using JavaFlorist.Models.Domain;
using JavaFlorist.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace JavaFlorist.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpPost]
        public IActionResult ApplyDiscount(Discount model)
        {
            string code = model.code;
            var data = _discountService.GetDiscountByCode(code);
            if (data != null)
            {
                if(data.count > 0)
                {
                    decimal des = data.decrease;
                    int id = data.discount_id;
                    return Json(new { discount = des, discount_id = id, message = "Apply Successfull !" }); 
                }
                else
                {
                    
                    return Json(new { discount = 0  , message = "Discount Over!"});
                }
               
            }
            return Json(new { discount = 0 }); 
        }

    }
}
