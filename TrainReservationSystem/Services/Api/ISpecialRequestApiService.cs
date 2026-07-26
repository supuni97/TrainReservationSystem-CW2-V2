using TrainReservationSystem.Models;

namespace TrainReservationSystem.Services.Api;

public interface ISpecialRequestApiService
{
    Task<List<SpecialRequest>> GetAll();

    Task<SpecialRequest?> GetById(int id);

    Task Add(SpecialRequest request);

    Task Update(SpecialRequest request);

    Task Delete(int id);
}