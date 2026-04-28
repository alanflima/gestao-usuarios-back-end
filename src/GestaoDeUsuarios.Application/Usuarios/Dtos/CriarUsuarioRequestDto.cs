namespace GestaoDeUsuarios.Application.Usuarios.Dtos;

public class CriarUsuarioRequestDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string? Cargo { get; set; }
}
