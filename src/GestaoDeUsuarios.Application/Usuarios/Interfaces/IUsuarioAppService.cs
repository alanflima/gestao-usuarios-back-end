using GestaoDeUsuarios.Application.Usuarios.Dtos;

namespace GestaoDeUsuarios.Application.Usuarios.Interfaces;

public interface IUsuarioAppService
{
    Task<UsuarioResponseDto> CriarAsync(CriarUsuarioRequestDto dto);
    Task<IEnumerable<UsuarioResponseDto>> ListarAtivosAsync();
    Task<UsuarioResponseDto> BuscarPorIdAsync(Guid id);
    Task<UsuarioResponseDto> AtualizarAsync(Guid id, AtualizarUsuarioRequestDto dto);
    Task DesativarAsync(Guid id);
}
