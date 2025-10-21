namespace Minimal_Api.DTOs;

public record VeiculoDTO
{
    public string Modelo { get; set; } = default!;
    public string Marca { get; set; } = default!;
    public int Ano { get; set; } = default!;
}