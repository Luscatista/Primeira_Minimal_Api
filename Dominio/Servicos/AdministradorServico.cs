using Minimal_Api.Dominio.Entidades;
using Minimal_Api.Dominio.Interfaces;
using Minimal_Api.DTOs;
using Minimal_Api.Infraestrutura.Db;

namespace Minimal_Api.Dominio.Servicos;

public class AdministradorServico : IAdministradorServico
{
    private readonly DbContexto _contexto;
    public AdministradorServico(DbContexto contexto)
    {
        _contexto = contexto;
    }
    public Administrador? Login(LoginDTO loginDTO)
    {
        var adm = _contexto.Admnistradores.FirstOrDefault(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
        if (adm == null)
            return null;

        return adm;        
    }    
}
