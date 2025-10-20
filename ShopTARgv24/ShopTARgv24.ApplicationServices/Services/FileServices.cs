using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly ShopTARgv24Context _context;
        private readonly IHostEnvironment _webHost;

        public FileServices(ShopTARgv24Context context, IHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // ====== 1) Сохранение файлов на диск + запись путей в FileToApis ======
        public void FilesToApi(SpaceshipDto dto, Spaceship spaceship)
        {
            if (dto?.Files == null || dto.Files.Count == 0)
                return;

            var uploadsFolder = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            foreach (var file in dto.Files)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                var path = new FileToApi
                {
                    // НЕ присваиваем Guid в int-Id! Предполагаем, что Id у FileToApi = Guid.
                    Id = Guid.NewGuid(),
                    ExistingFilePath = uniqueFileName,
                    SpaceshipId = spaceship.Id
                };

                _context.FileToApis.Add(path);
            }

            _context.SaveChanges();
        }

        public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        {
            var entity = await _context.FileToApis.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (entity == null) return null;

            var filePath = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload", entity.ExistingFilePath);
            if (File.Exists(filePath))
                File.Delete(filePath);

            _context.FileToApis.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            var removed = new List<FileToApi>();
            if (dtos == null || dtos.Length == 0) return removed;

            foreach (var dto in dtos)
            {
                var entity = await _context.FileToApis.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (entity == null) continue;

                var filePath = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload", entity.ExistingFilePath);
                if (File.Exists(filePath))
                    File.Delete(filePath);

                _context.FileToApis.Remove(entity);
                removed.Add(entity);
            }

            await _context.SaveChangesAsync();
            return removed;
        }

        // ====== 2) Сохранение файлов в БД (таблица FileToDatabases) ======
        public void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain)
        {
            if (dto?.Files == null || dto.Files.Count == 0)
                return;

            foreach (var file in dto.Files)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);

                var entity = new FileToDatabase
                {
                    // ВАЖНО: НЕ задаём Id, если он int (авто-инкремент)
                    // Если в модели есть свойство Name/ContentType — заполняем:
                    Name = file.FileName,
                    // ContentType = file.ContentType, // раскомментируй, если поле есть в модели
                    ImageData = ms.ToArray()
                    // Не ставим RealEstateId, если такого свойства нет в модели
                };

                _context.FileToDatabases.Add(entity);
            }

            _context.SaveChanges();
        }
    }
}
