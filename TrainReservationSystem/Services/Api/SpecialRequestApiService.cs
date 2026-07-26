using System.Net.Http.Json;
using TrainReservationSystem.Models;

namespace TrainReservationSystem.Services.Api;

public class SpecialRequestApiService(HttpClient httpClient) : ISpecialRequestApiService
{
    public async Task<List<SpecialRequest>> GetAll()
    {
        return await httpClient.GetFromJsonAsync<List<SpecialRequest>>("api/SpecialRequest")
               ?? new List<SpecialRequest>();
    }

    public async Task<SpecialRequest?> GetById(int id)
    {
        return await httpClient.GetFromJsonAsync<SpecialRequest>($"api/SpecialRequest/{id}");
    }

    public async Task Add(SpecialRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("api/SpecialRequest", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task Update(SpecialRequest request)
    {
        var response = await httpClient.PutAsJsonAsync($"api/SpecialRequest/{request.Id}", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task Delete(int id)
    {
        var response = await httpClient.DeleteAsync($"api/SpecialRequest/{id}");
        response.EnsureSuccessStatusCode();
    }
}