using GestaoDeUsuarios.Application.Usuarios.Dtos;

namespace GestaoDeUsuarios.Application.Usuarios.Interfaces;

public interface IUsuarioAppService
{
    Task<UsuarioResponseDto> CriarAsync(CriarUsuarioRequestDto dto);
    Task<IEnumerable<UsuarioResponseDto>> ListarAtivosAsync();
    Task<IEnumerable<UsuarioResponseDto>> ListarInativosAsync();
    Task<IEnumerable<UsuarioResponseDto>> PesquisarPorNomeAsync(string nome);
    Task<IEnumerable<UsuarioResponseDto>> PesquisarPorEmailAsync(string email);
    Task<UsuarioResponseDto> BuscarPorIdAsync(Guid id);
    Task<UsuarioResponseDto> AtualizarAsync(Guid id, AtualizarUsuarioRequestDto dto);
    Task DesativarAsync(Guid id);
}
