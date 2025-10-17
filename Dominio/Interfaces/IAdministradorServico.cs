using Minimal_Api.Dominio.Entidades;
using Minimal_Api.DTOs;

namespace Minimal_Api.Dominio.Interfaces;

public interface IAdministradorServico
{
    Administrador? Login(LoginDTO loginDTO);
}