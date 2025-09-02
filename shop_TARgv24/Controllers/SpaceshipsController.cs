using Microsoft.AspNetCore.Mvc;
using shop_TARgv24.Models.Spaceships;
using ShopTARgv24.Data;

namespace shop_TARgv24.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARgv24Context _context;

        public SpaceshipsController (
                ShopTARgv24Context context
            )
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(
                    x => new SpaceshipsIndexViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        BuiltDate = x.BuiltDate,
                        TypeName = x.TypeName
                    }
                );

            return View(result);  
        }
    }
}
