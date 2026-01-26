using ShopTARgv24.Core.Domain;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface IKindergartenServices
    {
        // Получить один садик по его ID
        Task<Kindergarten> GetAsync(Guid id);

        // Создать новый садик
        Task<Kindergarten> Create(Kindergarten dto);

        // Обновить данные садика
        Task<Kindergarten> Update(Kindergarten dto);

        // Удалить садик
        Task<Kindergarten> Delete(Guid id);
    }
}