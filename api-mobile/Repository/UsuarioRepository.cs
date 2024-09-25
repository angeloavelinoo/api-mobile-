using api_mobile.Data;
using api_mobile.Model;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.Repository
{
    public class UsuarioRepository(DataContext context) : Repository<Usuario>(context)
    {
        private readonly DataContext _context = context;

        public async Task<bool> AlredyExist(string email)
            => await _context.Set<Usuario>().AnyAsync(u => u.Ativo && u.Email == email);

        public async Task<Usuario> GetByEmail(string email)
        => await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Ativo && u.Email == email);
    }
}