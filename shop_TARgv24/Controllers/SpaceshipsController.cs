using Microsoft.AspNetCore.Mvc;

namespace shop_TARgv24.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARgv24Context _context;

        public SpaceshipsController(

            )
        {
            _context = context;
        public IActionResult Index()
        {
            var result = _context.

            return View();  
        }
    }
}
