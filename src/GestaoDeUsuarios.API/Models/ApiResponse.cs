namespace GestaoDeUsuarios.API.Models;

public class ApiResponse
{
    public bool Sucesso { get; set; }
    public object? DadosResposta { get; set; }
    public List<string> Erros { get; set; } = [];
    public string TimestampResposta { get; set; } = string.Empty;
    public string TempoDaResposta { get; set; } = string.Empty;
}
