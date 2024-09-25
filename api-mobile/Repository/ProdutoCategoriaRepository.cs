using api_mobile.Data;
using api_mobile.Model;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.Repository
{
    public class ProdutoCategoriaRepository(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task<ProdutoCategoria> Create(ProdutoCategoria obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<ProdutoCategoria> GetByProdutoId(int produtoId)
        {
            ProdutoCategoria produtoCategoria =  await _context.Set<ProdutoCategoria>().Include(x => x.Categoria).FirstOrDefaultAsync(x => x.ProdutoId == produtoId);

            return produtoCategoria;
        }
    }
}
