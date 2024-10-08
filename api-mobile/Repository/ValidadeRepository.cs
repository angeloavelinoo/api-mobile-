﻿using api_mobile.Data;
using api_mobile.Model;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.Repository;

public class ValidadeRepository(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task<Validade> Create(Validade validade)
    {
        await _context.Validades.AddAsync(validade);
        await _context.SaveChangesAsync();
        return validade;
    }

    public async Task<Validade> Get(int produtoId)
    {
        return await _context.Set<Validade>().Where(x => x.ProdutoId == produtoId).FirstOrDefaultAsync();
    }

    public async Task<Validade> GetByValidadeAndId(int produtoId, DateOnly date)
    {
        return await _context.Set<Validade>().Where(x => x.ProdutoId == produtoId && x.DataValidade == date).FirstOrDefaultAsync();
    }

    public async Task<List<Validade>> GetAll(int produtoId)
    {
        return await _context.Set<Validade>().Where(x => x.ProdutoId == produtoId && x.Quantidade > 0).ToListAsync();
    }

    public async Task<Validade> Update(Validade validade)
    {
        _context.Update(validade);
        await _context.SaveChangesAsync();
        return validade;
    }

}