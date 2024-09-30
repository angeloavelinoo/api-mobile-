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
            List<Produto> produtos = await _context.Set<Produto>().Where(x => x.EstoqueId == estoqueId && x.Ativo).ToListAsync();

            return produtos;
        }

        public async Task<bool> AlredyExist(string codBarra)
        {
            return await _context.Set<Produto>().Where(x => x.CodigoBarras == codBarra).AnyAsync();
        }

        public async Task<Produto> GetByCodBarras(string codigoBarra)
        {
            Produto produto = await _context.Set<Produto>().Where(x => x.CodigoBarras == codigoBarra).FirstOrDefaultAsync();
            
            return produto; 
        }

        public async Task<List<Produto>> ListaCompra()
        {
            List<Produto> produtos = await _context.Set<Produto>().Where(x => x.Quantidade < x.QuantidadeMinima).ToListAsync();

            return produtos;
        }
    }
}
