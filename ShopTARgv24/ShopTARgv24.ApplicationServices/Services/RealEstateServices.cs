using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly ShopTARgv24Context _context;
        private readonly IFileServices _fileServices;

        public RealEstateServices(
            ShopTARgv24Context context,
            IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            var domain = new RealEstate
            {
                Id = Guid.NewGuid(),
                Area = dto.Area,
                Location = dto.Location,
                RoomNumber = dto.RoomNumber,
                BuildingType = dto.BuildingType,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            // файлы в БД (если есть)
            if (dto.Files != null && dto.Files.Count > 0)
                _fileServices.UploadFilesToDatabase(dto, domain);

            await _context.RealEstates.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<RealEstate?> DetailAsync(Guid id)
        {
            // при желании можно добавить Include к изображениям
            return await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            // обновляем существующую запись (а не создаём новую «пустую»)
            var domain = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (domain == null)
                return null!; // или брось исключение, если по контракту не допускается null

            domain.Area = dto.Area;
            domain.Location = dto.Location;
            domain.RoomNumber = dto.RoomNumber;
            domain.BuildingType = dto.BuildingType;
            domain.CreatedAt = dto.CreatedAt;
            domain.ModifiedAt = DateTime.Now;

            if (dto.Files != null && dto.Files.Count > 0)
                _fileServices.UploadFilesToDatabase(dto, domain);

            _context.RealEstates.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<RealEstate?> Delete(Guid id)
        {
            var realestate = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            if (realestate == null)
                return null;

            _context.RealEstates.Remove(realestate);
            await _context.SaveChangesAsync();

            return realestate;
        }
    }
}
