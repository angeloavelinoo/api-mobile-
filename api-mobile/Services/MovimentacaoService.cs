using api_mobile.DTOs;
using api_mobile.Model;
using api_mobile.Repository;
using api_mobile.ViewModel;
using System.Net;

namespace api_mobile.Services
{
    public class MovimentacaoService(MovimentacaoRepository movimentacaoRepository, EstoqueRepository estoqueRepository, ProdutoRepository produtoRepository)
    {
        private readonly MovimentacaoRepository _movimentacaoRepository = movimentacaoRepository;
        private readonly EstoqueRepository _estoqueRepository = estoqueRepository;
        private readonly ProdutoRepository _produtoRepository = produtoRepository;

        public async Task<ResultModel<List<MovimentacaoDTO>>> GetAll(int usuarioId)
        {
            List<Movimentacao> movimentacao = new();

            List<Estoque> estoque = await _estoqueRepository.GetByUsuario(usuarioId);

            List<Produto> produtos = new();

            List<MovimentacaoDTO> movimentacaoDTO = new();

            foreach (var item in estoque)
            {
                List<Produto> produto = await _produtoRepository.GetByEstoque(item.Id);

                foreach (var prod in produto)
                {
                    produtos.Add(prod);
                }
            }

            foreach (var item in produtos)
            {
                List<Movimentacao> mov = await _movimentacaoRepository.GetByProdutoId(item.Id);

                foreach (var movi in mov)
                {
                    movimentacao.Add(movi);
                }
            }

            foreach (var mov in movimentacao)
            {
                MovimentacaoDTO movDTO = new()
                {
                    Id = mov.Id,
                    ProdutoId = mov.ProdutoId,
                    Quantidade = mov.Quantidade,
                    Tipo = mov.Tipo,
                    DataMovimentacao = mov.DataMovimentacao,
                    ValorTotal = mov.ValorTotal
                };

                movimentacaoDTO.Add(movDTO);

                if (movimentacao.Count == 0)
                {
                    return new ResultModel<List<MovimentacaoDTO>>(HttpStatusCode.NotFound, "Nenhuma movimentação encontrada");
                }


            }
            return new(movimentacaoDTO);

        }
    }
}
