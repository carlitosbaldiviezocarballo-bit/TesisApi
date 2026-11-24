using Microsoft.AspNetCore.Mvc;
using TesisApi.DTOs;
using TesisApi.Contracts.Services;

namespace TesisApi.Controllers;

[ApiController]
[Route("api/tutores")]
public class TutoresController : ControllerBase
{
    private readonly ITutorService _service;
    public TutoresController(ITutorService service) => _service = service;

    /// <summary>1) POST Tutor</summary>
    [HttpPost]
    [ProducesResponseType(typeof(TutorResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] TutorCreateDto dto)
    {
        var creado = await _service.CrearAsync(dto);
        return CreatedAtAction(nameof(Get), null, creado);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TutorResponseDto>>> Get() =>
        Ok(await _service.ListarAsync());
}
