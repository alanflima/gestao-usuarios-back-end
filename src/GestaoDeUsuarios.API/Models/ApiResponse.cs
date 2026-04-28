namespace GestaoDeUsuarios.API.Models;

public class ApiResponse
{
    public object? DadosResposta { get; set; }
    public string TimestampResposta { get; set; } = string.Empty;
    public string TempoDaResposta { get; set; } = string.Empty;
}
