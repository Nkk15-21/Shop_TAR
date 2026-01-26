using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24.ApplicationServices
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopTARgv24Context _context;

        public KindergartenServices(ShopTARgv24Context context)
        {
            _context = context;
        }

        // 1. Получаем садик из базы
        public async Task<Kindergarten> GetAsync(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        // 2. Создаем новый садик
        public async Task<Kindergarten> Create(Kindergarten dto)
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

            await _context.Kindergartens.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        // 3. Обновляем данные
        public async Task<Kindergarten> Update(Kindergarten dto)
        {
            _context.Kindergartens.Update(dto);
            await _context.SaveChangesAsync();

            return dto;
        }

        // 4. Удаляем садик
        public async Task<Kindergarten> Delete(Guid id)
        {
            var kindergarten = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Kindergartens.Remove(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }
    }
}