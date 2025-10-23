using Minimal_Api.Dominio.Entidades;
using Minimal_Api.DTOs;

namespace Minimal_Api.Dominio.Interfaces;

public interface IAdministradorServico
{
    Administrador? Login(LoginDTO loginDTO);
    Administrador Incluir(Administrador administrador);
    List<Administrador>? Todos(int? pagina = 1);
    Administrador? BuscarPorId(int id);
}