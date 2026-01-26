using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.Controllers
{
    public class KindergartensController : Controller
    {
        private readonly IKindergartenServices _kindergartenServices;

        // Конструктор: просим программу дать нам сервис для работы
        public KindergartensController(IKindergartenServices kindergartenServices)
        {
            _kindergartenServices = kindergartenServices;
        }

        // 1. Список всех садиков (Главная страница раздела)
        public IActionResult Index()
        {
            // Тут мы позже добавим получение списка из базы
            return View();
        }

        // 2. Страница создания (просто открывает форму)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. Сохранение нового садике (срабатывает при нажатии кнопки "Save")
        [HttpPost]
        public async Task<IActionResult> Create(Kindergarten vm)
        {
            var result = await _kindergartenServices.Create(vm);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // 1. Открываем страницу редактирования
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var kindergarten = await _kindergartenServices.GetAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            return View(kindergarten);
        }

        // 2. Сохраняем изменения
        [HttpPost]
        public async Task<IActionResult> Edit(Kindergarten vm)
        {
            var result = await _kindergartenServices.Update(vm);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _kindergartenServices.Delete(id);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}