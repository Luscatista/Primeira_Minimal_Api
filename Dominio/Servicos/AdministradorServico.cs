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
    public List<Administrador>? Todos(int? pagina = 1)
    {
        var query = _contexto.Administradores.AsQueryable();
        int itensPorPagina = 10;

        // var total = query.Count();
        // var paginasTotais = (int)Math.Ceiling(total / (double)itensPorPagina);

        // var resultado = query
        // .Skip(((int)pagina - 1) * itensPorPagina)
        // .Take(itensPorPagina)
        // .ToList();
        if (pagina != null)
        {
            query = query
            .Skip(((int)pagina - 1) * itensPorPagina)
            .Take(itensPorPagina);
        }

        return query.ToList();
    }
    public Administrador Incluir(Administrador administrador)
    {
        _contexto.Administradores.Add(administrador);
        _contexto.SaveChanges();

        return administrador;
    }
    public Administrador? Login(LoginDTO loginDTO)
    {
        var adm = _contexto.Administradores.FirstOrDefault(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
        return adm;
    }

    public Administrador? BuscarPorId(int id)
    {
        var administrador = _contexto.Administradores.Find(id);
        return administrador;
    }
}
