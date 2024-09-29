using api_mobile.DTOs.EstoquesDTO;
using api_mobile.DTOs.ProdutosDTO;
using api_mobile.Model;
using api_mobile.Repository;
using api_mobile.ViewModel;
using System.Net;

namespace api_mobile.Services
{
    public class EstoqueService(EstoqueRepository estoqueRepository, ProdutoRepository produtoRepository)
    {
        private readonly EstoqueRepository _estoqueRepository = estoqueRepository;
        private readonly ProdutoRepository _produtoRepository = produtoRepository;

        public async Task<ResultModel<dynamic>> Add(EstoqueCreateDTO estoqueDTO, int usuarioId)
        {
            Estoque estoque = new(nome: estoqueDTO.Nome, usuarioId: usuarioId, ativo: true);

            await _estoqueRepository.Create(estoque);

            return new();
        }

        public async Task<ResultModel<List<EstoqueListDTO>>> GetAll(int usuarioId)
        {
            List<Estoque> estoques = await _estoqueRepository.GetByUsuario(usuarioId);

            List<EstoqueListDTO> estoquesDTO = new();

            foreach (Estoque estoque in estoques)
            {
                estoquesDTO.Add(new()
                {
                    Id = estoque.Id,
                    Nome = estoque.Nome
                });
            }

            return new(estoquesDTO);
        }

        public async Task<ResultModel<EstoqueDTO>> GetById(int id)
        {
            Estoque estoque = await _estoqueRepository.GetById(id);
            if (estoque == null)
                return new(HttpStatusCode.NotFound, "Estoque não encontrado");

            List<Produto> produtos = await _produtoRepository.GetByEstoque(estoque.Id);
            List<ProdutoDTO> produtosDTO = new();

            foreach (Produto produto in produtos)
            {
                produtosDTO.Add(new()
                {
                    Id = produto.Id,
                    CodigoBarras = produto.CodigoBarras,
                    Nome = produto.Nome,
                    QuantidadeTotal = produto.Quantidade,
                    QuantidadeMinima = produto.QuantidadeMinima,
                    Valor = produto.Valor,
                    EstoqueId = produto.EstoqueId
                });
            }

            EstoqueDTO estoqueDTO = new()
            {
                Id = estoque.Id,
                Nome = estoque.Nome,
                Produtos = produtosDTO
            };

            return new(estoqueDTO);
        }


        public async Task<ResultModel<dynamic>> Update(int id,EstoqueCreateDTO estoqueCreateDTO)
        {
            Estoque estoque = await _estoqueRepository.GetById(id);
            if (estoque == null)
                return new(HttpStatusCode.NotFound, "Estoque não encontrado");

            estoque.Nome = estoqueCreateDTO.Nome;

            await _estoqueRepository.Update(estoque);

            return new();
        }

        public async Task<ResultModel<dynamic>> Delete(int id)
        {
            Estoque estoque = await _estoqueRepository.GetById(id);
            if (estoque == null)
                return new(HttpStatusCode.NotFound, "Estoque não encontrado");

            List<Produto> produtos = await _produtoRepository.GetByEstoque(estoque.Id);

            foreach (Produto produto in produtos)
            {
                await _produtoRepository.Remove(produto);
            }

            await _estoqueRepository.Remove(estoque);

            

            return new();
        }
    }

}
