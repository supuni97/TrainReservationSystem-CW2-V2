using SpecialRequest.Api.Models;

namespace SpecialRequest.Api.Repositories;

public interface ISpecialRequestRepository
{
    IEnumerable<SpecialRequest> GetAll();

    SpecialRequest? GetById(int id);

    SpecialRequest Add(SpecialRequest request);

    bool Update(SpecialRequest request);

    bool Delete(int id);
}