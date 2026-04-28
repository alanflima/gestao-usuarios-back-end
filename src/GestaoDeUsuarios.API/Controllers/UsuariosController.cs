using GestaoDeUsuarios.Application.Usuarios.Dtos;
using GestaoDeUsuarios.Application.Usuarios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeUsuarios.API.Controllers;

[ApiController]
[Route("api/v1/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioAppService _usuarioAppService;

    public UsuariosController(IUsuarioAppService usuarioAppService)
    {
        _usuarioAppService = usuarioAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarUsuarioRequestDto dto)
    {
        var resultado = await _usuarioAppService.CriarAsync(dto);
        return CreatedAtAction(nameof(BuscarPorIdAsync), new { id = resultado.Id }, resultado);
    }

    [HttpGet]
    public async Task<IActionResult> ListarAtivosAsync()
    {
        var resultado = await _usuarioAppService.ListarAtivosAsync();
        return Ok(resultado);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> BuscarPorIdAsync(Guid id)
    {
        var resultado = await _usuarioAppService.BuscarPorIdAsync(id);
        return Ok(resultado);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarAsync(Guid id, [FromBody] AtualizarUsuarioRequestDto dto)
    {
        var resultado = await _usuarioAppService.AtualizarAsync(id, dto);
        return Ok(resultado);
    }

    [HttpPatch("{id:guid}/desativar")]
    public async Task<IActionResult> DesativarAsync(Guid id)
    {
        await _usuarioAppService.DesativarAsync(id);
        return NoContent();
    }
}
