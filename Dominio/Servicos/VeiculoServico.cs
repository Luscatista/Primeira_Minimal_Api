using Microsoft.EntityFrameworkCore;
using Minimal_Api.Dominio.Entidades;
using Minimal_Api.Dominio.Interfaces;
using Minimal_Api.Infraestrutura.Db;

namespace Minimal_Api.Dominio.Servicos;

public class VeiculoServico : IVeiculoServico
{
    private readonly DbContexto _contexto;
    public VeiculoServico(DbContexto contexto)
    {
        _contexto = contexto;
    }
    public List<Veiculo>? Todos(int? pagina = 1, string? modelo = null, string? marca = null)
    {
        var query = _contexto.Veiculos.AsQueryable();
        if(!string.IsNullOrEmpty(modelo))
        {
            query = query.Where(v => EF.Functions.Like(v.Modelo.ToLower(), $"%{modelo}%"));
        }

        int itensPorPagina = 10;

        if(pagina != null)
            query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);

        return query.ToList();
    }
    public Veiculo? BuscarPorId(int id)
    {
        var veiculo = _contexto.Veiculos.Find(id);       

        return veiculo;
    }
    public Veiculo Incluir(Veiculo veiculo)
    {
        _contexto.Veiculos.Add(veiculo);
        _contexto.SaveChanges();

        return veiculo;
    }
     public Veiculo? Atualizar(Veiculo veiculo)
    {
        var veiculoAtual = _contexto.Veiculos.Find(veiculo.Id);
        if (veiculoAtual == null)
            return null;

        _contexto.Veiculos.Update(veiculo);
        _contexto.SaveChanges();
        
        return veiculo;
    }
     public Veiculo? Apagar(Veiculo veiculo)
    {
        var veiculoAtual = _contexto.Veiculos.Find(veiculo.Id);
        if (veiculoAtual == null)
            return null;

        _contexto.Veiculos.Remove(veiculo);
        _contexto.SaveChanges();

        return veiculo;
    }
}