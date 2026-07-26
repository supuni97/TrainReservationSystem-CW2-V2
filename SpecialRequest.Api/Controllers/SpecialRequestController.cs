using Microsoft.AspNetCore.Mvc;
using SpecialRequest.Api.Models;
using SpecialRequest.Api.Repositories;

namespace SpecialRequest.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialRequestController : ControllerBase
{
    private readonly ISpecialRequestRepository _repository;

    public SpecialRequestController(ISpecialRequestRepository repository)
    {
        _repository = repository;
    }

    // GET: api/SpecialRequest
    [HttpGet]
    public ActionResult<IEnumerable<SpecialRequestModel>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    // GET: api/SpecialRequest/5
    [HttpGet("{id}")]
    public ActionResult<SpecialRequestModel> GetById(int id)
    {
        var request = _repository.GetById(id);

        if (request == null)
            return NotFound();

        return Ok(request);
    }

    // POST: api/SpecialRequest
    [HttpPost]
    public ActionResult<SpecialRequestModel> Create(SpecialRequestModel request)
    {
        var created = _repository.Add(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created);
    }

    // PUT: api/SpecialRequest/5
    [HttpPut("{id}")]
    public IActionResult Update(int id, SpecialRequestModel request)
    {
        if (id != request.Id)
            return BadRequest();

        var updated = _repository.Update(request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/SpecialRequest/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _repository.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}