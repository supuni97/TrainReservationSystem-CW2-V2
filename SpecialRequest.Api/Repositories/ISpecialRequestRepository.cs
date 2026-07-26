using SpecialRequest.Api.Models;

namespace SpecialRequest.Api.Repositories;

public interface ISpecialRequestRepository
{
    IEnumerable<SpecialRequestModel> GetAll();

    SpecialRequestModel? GetById(int id);

    SpecialRequestModel Add(SpecialRequestModel request);

    bool Update(SpecialRequestModel request);

    bool Delete(int id);
}