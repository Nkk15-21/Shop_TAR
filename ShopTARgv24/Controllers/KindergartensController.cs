using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using System;
using System.Threading.Tasks;

namespace ShopTARgv24.Controllers
{
    public class KindergartensController : Controller
    {
        private readonly IKindergartenServices _kindergartenServices;
        private readonly IFileServices _fileServices;

        public KindergartensController(IKindergartenServices kindergartenServices, IFileServices fileServices)
        {
            _kindergartenServices = kindergartenServices;
            _fileServices = fileServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create(KindergartenDto dto)
        {
            var domain = new Kindergarten()
            {
                Id = Guid.NewGuid(),
                GroupName = dto.GroupName,
                ChildrenCount = dto.ChildrenCount,
                KindergartenName = dto.KindergartenName,
                TeacherName = dto.TeacherName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }

            var result = await _kindergartenServices.Create(domain);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            var result = await _fileServices.RemoveImageFromDatabase(id);
            if (result == null) return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index)); // Или вернись на страницу Edit
        }
    }
}