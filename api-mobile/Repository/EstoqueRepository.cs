using api_mobile.Data;
using api_mobile.Model;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.Repository
{
    public class EstoqueRepository(DataContext context) : Repository<Estoque>(context)
    {
        private readonly DataContext _context = context;

        public async Task<List<Estoque>> GetByUsuario(int usuarioId)
        {
            List<Estoque> estoque = await _context.Set<Estoque>().Where (x => x.UsuarioId == usuarioId && x.Ativo).ToListAsync();

            return estoque;
        }
    }
}
