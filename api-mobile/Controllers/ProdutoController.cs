using api_mobile.DTOs.ProdutosDTO;
using api_mobile.Model;
using api_mobile.Services;
using api_mobile.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace api_mobile.Controllers
{
    public class ProdutoController(ProdutoService produtoService) : BaseApiController
    {
        private readonly ProdutoService _produtoService = produtoService;



        [HttpPost]
        public async Task<IActionResult> Add(ProdutoCreateDTO produtoDTO)
        {
            return ServiceResponse(await _produtoService.Add(produtoDTO));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return ServiceResponse(await _produtoService.GetById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int estoqueId)
        {
            return ServiceResponse(await _produtoService.GetAll(estoqueId));
        }

        [HttpGet("CodBarras/{codigoBarra}")]
        public async Task<IActionResult> GetByCodigoBarra(string codigoBarra)
        {
            return ServiceResponse(await _produtoService.GetByCodBarras(codigoBarra));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProdutoUpdateDTO produtoDTO)
        {
            return ServiceResponse(await _produtoService.Update(id, produtoDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ServiceResponse(await _produtoService.Delete(id));
        }

        [HttpGet("ListaCompra")]
        public async Task<IActionResult> ListaCompra()
        {
            return ServiceResponse(await _produtoService.ListaCompra());
        }
    }
}
