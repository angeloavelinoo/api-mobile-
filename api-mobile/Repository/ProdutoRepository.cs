using api_mobile.Data;
using api_mobile.Model;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.Repository
{
    public class ProdutoRepository(DataContext context) : Repository<Produto>(context)
    {
        private readonly DataContext _context = context;

        public async Task<List<Produto>> GetByEstoque(int estoqueId)
        {
            List<Produto> produtos = await _context.Set<Produto>().Where(x => x.EstoqueId == estoqueId).ToListAsync();

            return produtos;
        }
    }
}
