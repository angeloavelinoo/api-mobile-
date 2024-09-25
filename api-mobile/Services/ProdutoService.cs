using api_mobile.DTOs.ProdutosDTO;
using api_mobile.Model;
using api_mobile.Repository;
using api_mobile.ViewModel;
using System.Net;

namespace api_mobile.Services
{
    public class ProdutoService(ProdutoRepository produtoRepository, ProdutoCategoriaRepository produtoCategoriaRepository)
    {
        private readonly ProdutoRepository _produtoRepository = produtoRepository;
        private readonly ProdutoCategoriaRepository _produtoCategoriaRepository = produtoCategoriaRepository;

        public async Task<ResultModel<dynamic>> Add(ProdutoCreateDTO produtoDTO)
        {
            Produto produto = new(codigoBarras: produtoDTO.CodigoBarras, nome: produtoDTO.Nome, quantidade: produtoDTO.Quantidade, quantidadeMinima: produtoDTO.QuantidadeMinima, valor: produtoDTO.Valor, estoqueId: produtoDTO.EstoqueId, ativo: true);

            await _produtoRepository.Create(produto);

            ProdutoCategoria produtoCategoria = new(produtoId: produto.Id, categoriaId: produtoDTO.CategpriaId);

            await _produtoCategoriaRepository.Create(produtoCategoria);

            return new();


        }

        //public async Task<ResultModel<dynamic>> Update(int id, ProdutoUpdateDTO produtoDTO)
        //{

        //}

        public async Task<ResultModel<ProdutoDTO>> GetById(int id)
        {
            Produto produto = await _produtoRepository.GetById(id);
            if (produto == null)
                return new(HttpStatusCode.NotFound, "Produto não encontrado");

            ProdutoCategoria produtoCategoria = await _produtoCategoriaRepository.GetByProdutoId(produto.Id);

            ProdutoDTO produtoDTO = new()
            {
                Id = produto.Id,
                CodigoBarras = produto.CodigoBarras,
                Nome = produto.Nome,
                Quantidade = produto.Quantidade,
                QuantidadeMinima = produto.QuantidadeMinima,
                Valor = produto.Valor,
                EstoqueId = produto.EstoqueId,
                CategoriaNome = produtoCategoria.Categoria.Nome
            };

            return new(produtoDTO);
        }

        public async Task<ResultModel<List<ProdutoDTO>>> GetAll(int estoqueId)
        {
            List<Produto> produtos = await _produtoRepository.GetByEstoque(estoqueId);

            if(produtos.Count == 0)
                return new(HttpStatusCode.NotFound, "Nenhum produto deste estoque foi encontrado");

            List <ProdutoDTO> produtosDTO = new();

            foreach(Produto produto in produtos)
            {
                ProdutoCategoria produtoCategoria = await _produtoCategoriaRepository.GetByProdutoId(produto.Id);

                ProdutoDTO produtoDTO = new ProdutoDTO {
                    Id = produto.Id,
                    CodigoBarras = produto.CodigoBarras,
                    Nome = produto.Nome,
                    Quantidade = produto.Quantidade,
                    QuantidadeMinima = produto.QuantidadeMinima,
                    Valor = produto.Valor,
                    EstoqueId = produto.EstoqueId,
                    CategoriaNome = produtoCategoria.Categoria.Nome
                };

                produtosDTO.Add(produtoDTO);
            }

            return new(produtosDTO);
        }

        public async Task<ResultModel<dynamic>> Delete(int id)
        {
            Produto produto = await _produtoRepository.GetById(id);

            if (produto == null) return new(HttpStatusCode.NotFound, "Produto não encontrado");

            await _produtoRepository.Remove(produto);

            return new();
        }
    }
}
