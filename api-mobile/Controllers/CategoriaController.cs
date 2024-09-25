using api_mobile.DTOs.CategoriasDTO;
using api_mobile.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_mobile.Controllers
{
    public class CategoriaController(CategoriaService categoriaService) : BaseApiController
    {
        private readonly CategoriaService _categoriaService = categoriaService;

        [HttpPost]
        public async Task<IActionResult> Add(CategoriaCreateDTO categoriaDTO)
        {
            return ServiceResponse(await _categoriaService.Add(categoriaDTO));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ServiceResponse(await _categoriaService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return ServiceResponse(await _categoriaService.GetById(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoriaCreateDTO categoriaDTO, int id)
        {
            return ServiceResponse(await _categoriaService.Update(id, categoriaDTO));
        }

        [HttpDelete]

        public async Task<IActionResult> Remove(int id)
        {
            return ServiceResponse(await _categoriaService.Remove(id));
        }
    }
}
