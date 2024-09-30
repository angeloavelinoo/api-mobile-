using System.Data;
using api_mobile.Data;
using api_mobile.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.Repository;

public class MovimentacaoRepository(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<Movimentacao> Create(Movimentacao movimentacao)
    {
        await _context.AddAsync(movimentacao);
        await _context.SaveChangesAsync();
        return movimentacao;
    }

    public async Task<Movimentacao> GetById(int id)
    {
        var movimentacao = await _context.Set<Movimentacao>().Where(x => x.Id == id).FirstAsync();
        return movimentacao;
    }

    public async Task<List<Movimentacao>> GetAll()
    {
        List<Movimentacao> movimentacoes = await _context.Set<Movimentacao>().ToListAsync();
        
        return movimentacoes;
    }

    public async Task<List<Movimentacao>> GetByProdutoId(int produtoId)
    {
        List<Movimentacao> movimentacoes = await _context.Set<Movimentacao>().Where(x => x.ProdutoId == produtoId).ToListAsync();
        
        return movimentacoes;
    }
}