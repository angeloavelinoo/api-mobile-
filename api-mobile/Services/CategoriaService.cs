using api_mobile.DTOs.CategoriasDTO;
using api_mobile.Model;
using api_mobile.Repository;
using api_mobile.ViewModel;
using System.Net;

namespace api_mobile.Services
{
    public class CategoriaService(CategoriaRepository categoriaRepository)
    {
        private readonly CategoriaRepository _categoriaRepository = categoriaRepository;

        public async Task<ResultModel<dynamic>> Add(CategoriaCreateDTO categoriaDTO)
        {
            Categoria categoria = new Categoria
            {
                Nome = categoriaDTO.Nome
            };

             await _categoriaRepository.Create(categoria);

            return new();
        }

        public async Task<ResultModel<Categoria>> GetById(int id)
        {
            Categoria categoria = await _categoriaRepository.GetById(id);
            if(categoria == null)
                return new(HttpStatusCode.NotFound, "Categoria não encontrada");

            return new(categoria);
        }

        public async Task<ResultModel<IList<Categoria>>> GetAll()
        {
            IList<Categoria> categorias = await _categoriaRepository.GetItens();
            if (categorias.Count == 0)
                return new(HttpStatusCode.NotFound, "Nenhuma categoria encontrada");

            return new(categorias);
        }

        public async Task<ResultModel<Categoria>> Update(int id, CategoriaCreateDTO categoriaDTO)
        {
            Categoria categoria = await _categoriaRepository.GetById(id);
            if (categoria == null)
                return new(HttpStatusCode.NotFound, "Categoria não encontrada");

            categoria.Nome = categoriaDTO.Nome;

            await _categoriaRepository.Update(categoria);

            return new();
        }

        public async Task<ResultModel<dynamic>> Remove(int id)
        {
            Categoria categoria = await _categoriaRepository.GetById(id);
            if (categoria == null)
                return new(HttpStatusCode.NotFound, "Categoria não encontrada");

             await _categoriaRepository.Remove(categoria);

            return new();
        }
    }
}
