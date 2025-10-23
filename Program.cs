using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minimal_Api.Dominio.Entidades;
using Minimal_Api.Dominio.Enums;
using Minimal_Api.Dominio.Interfaces;
using Minimal_Api.Dominio.ModelViews;
using Minimal_Api.Dominio.Servicos;
using Minimal_Api.DTOs;
using Minimal_Api.Infraestrutura.Db;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
    builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});
#endregion

#region App
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
#endregion

#region Administradores

app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
{
    if (administradorServico.Login(loginDTO) != null)
        return Results.Ok("Login realizado com sucesso");
    else
        return Results.Unauthorized();
}).WithTags("Administradores");

app.MapGet("/administradores", ([FromQuery] int? pagina, IAdministradorServico administradorServico) =>
{
    // var adms = new List<Administrador>();
    // var administradores = administradorServico.Todos(pagina);

    // foreach(var adm in administradores)
    // {
    //     adms.Add(new Administrador
    //     {
    //         Id = adm.Id,
    //         Email = adm.Email,
    //         Senha = adm.Senha,
    //         Perfil = adm.Perfil
    //     });
    // }
    return Results.Ok(administradorServico.Todos(pagina));
}).WithTags("Administradores");

app.MapGet("/adminitradores/{id}", ([FromQuery] int id, IAdministradorServico administradorServico) =>
{
    var administrador = administradorServico.BuscarPorId(id);

    if (administrador == null)
        return Results.NotFound();

    return Results.Ok(administrador);
}).WithTags("Administradores");

app.MapPost("/administradores", ([FromBody] AdministradorDTO administradorDTO, IAdministradorServico administradorServico) =>
{
    var validacao = new ErrosDeValidacao
    {
        Mensagens = new List<string>()
    };

    if (string.IsNullOrEmpty(administradorDTO.Email))
        validacao.Mensagens.Add("O campo E-mail é obrigatório");
    if (string.IsNullOrEmpty(administradorDTO.Senha))
        validacao.Mensagens.Add("O campo Senha é obrigatório");
    if (administradorDTO.Perfil == null)
        validacao.Mensagens.Add("O campo Perfil é obrigatório");

    if (validacao.Mensagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }

    var administrador = new Administrador
    {
        Email = administradorDTO.Email,
        Senha = administradorDTO.Senha,
        Perfil = administradorDTO.Perfil.ToString() ?? Perfil.Editor.ToString()
    };

    administradorServico.Incluir(administrador);

    return Results.Created($"/administrador/{administrador.Id}", administrador);

}).WithTags("Administradores");

#endregion

#region Veiculos

ErrosDeValidacao validaDTO(VeiculoDTO veiculoDTO)
{
    var validacao = new ErrosDeValidacao
    {
        Mensagens = new List<string>()
    };

    if (string.IsNullOrEmpty(veiculoDTO.Modelo))
        validacao.Mensagens.Add("O campo Modelo obrigatório");

    if (string.IsNullOrEmpty(veiculoDTO.Marca))
        validacao.Mensagens.Add("O campo Marca obrigatório");

    if (veiculoDTO.Ano < 1950)
        validacao.Mensagens.Add("Permitido apenas da ano de 1950 em diante");

    return validacao;
}

app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{
    var validacao = validaDTO(veiculoDTO);
    if (validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);

    var veiculo = new Veiculo
    {
        Modelo = veiculoDTO.Modelo,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano
    };

    veiculoServico.Incluir(veiculo);

    return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
}).WithTags("Veículos");

app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
{
    return Results.Ok(veiculoServico.Todos(pagina));
}).WithTags("Veículos");

app.MapGet("/veiculos/{id}", ([FromQuery] int id, IVeiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.BuscarPorId(id);

    if (veiculo == null)
        return Results.NotFound();

    return Results.Ok(veiculo);
}).WithTags("Veículos");

app.MapPut("/veiculos/{id}", ([FromQuery] int id, VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{   
    var veiculo = veiculoServico.BuscarPorId(id);
    if (veiculo == null)
        return Results.NotFound();

    var validacao = validaDTO(veiculoDTO);
    if (validacao.Mensagens.Count > 0)
        return Results.BadRequest(validacao);

    veiculo.Modelo = veiculoDTO.Modelo;
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Ano = veiculoDTO.Ano;

    veiculoServico.Atualizar(veiculo);

    return Results.Ok(veiculo);
}).WithTags("Veículos");

app.MapDelete("/veiculos/{id}", ([FromQuery] int id, IVeiculoServico veiculoServico) => {

    var veiculo = veiculoServico.BuscarPorId(id);
    if (veiculo == null)
        return Results.NotFound();

    veiculoServico.Apagar(veiculo);

    return Results.NoContent();
}).WithTags("Veículos");
#endregion

app.Run();