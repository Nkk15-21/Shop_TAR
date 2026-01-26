using ShopTARgv24.Core.Domain;
using ShopTARgv24.Core.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface IFileServices
    {
        // Методы для API (Spaceship)
        void FilesToApi(SpaceshipDto dto, Spaceship spaceship);
        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos); // Исправлено для скриншота 16

        // Методы для БД (RealEstate и Kindergarten)
        void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain);
        void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain); // Новый метод
        Task<FileToDatabase> RemoveImageFromDatabase(Guid id); // Удаление одного фото
    }
}