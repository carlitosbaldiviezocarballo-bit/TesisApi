using Microsoft.AspNetCore.Mvc;
using TesisApi.DTOs;
using TesisApi.Contracts.Services;

namespace TesisApi.Controllers;

[ApiController]
[Route("api/tesis")]
public class TesisController : ControllerBase
{
    private readonly ITesisService _service;
    public TesisController(ITesisService service) => _service = service;

    /// <summary>2) POST Tesis</summary>
    [HttpPost]
    [ProducesResponseType(typeof(TesisResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] TesisCreateDto dto)
    {
        var creada = await _service.CrearAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada);
    }

    /// <summary>3) Buscar tesis y mostrar tutor. Sin parámetro devuelve todas.</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TesisResponseDto>>> Buscar([FromQuery] string? q) =>
        Ok(await _service.BuscarAsync(q));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TesisResponseDto>> GetById(int id) =>
        Ok(await _service.ObtenerAsync(id));

    /// <summary>4) Subir imagen de una tesis (multipart/form-data, campo "archivo").</summary>
    [HttpPost("{id:int}/imagen")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<TesisResponseDto>> SubirImagen(int id, IFormFile archivo) =>
        Ok(await _service.SubirImagenAsync(id, archivo));
}
