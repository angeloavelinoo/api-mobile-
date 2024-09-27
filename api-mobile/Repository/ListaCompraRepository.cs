using api_mobile.Data;
using api_mobile.Model;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.Repository
{
    public class ListaCompraRepository(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task<List<ListaCompra>> ListaCompra(int produtoId)
        {
            return await _context.Set<ListaCompra>().Where(x => x.ProdutoId == produtoId).ToListAsync();
        }
    }
}
