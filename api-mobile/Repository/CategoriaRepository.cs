using api_mobile.Data;
using api_mobile.Model;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.Repository
{
    public class CategoriaRepository(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task<Categoria> Create(Categoria obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<IList<Categoria>> GetItens()
        {
            return await _context.Set<Categoria>().ToListAsync();
        }


        public async Task<Categoria> GetById(int? id)
        {
            return await _context.Set<Categoria>().FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Categoria> Update(Categoria obj)
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<Categoria> Remove(Categoria obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

    }
    
}
