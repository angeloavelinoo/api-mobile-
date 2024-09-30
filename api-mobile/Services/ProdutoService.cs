using api_mobile.DTOs.ProdutosDTO;
using api_mobile.Model;
using api_mobile.Repository;
using api_mobile.ViewModel;
using System.Net;
using api_mobile.DTOs;

namespace api_mobile.Services
{
    public class ProdutoService(ProdutoRepository produtoRepository, ProdutoCategoriaRepository produtoCategoriaRepository, ValidadeRepository validadeRepository, MovimentacaoRepository movimentacaoRepository)
    {
        private readonly ProdutoRepository _produtoRepository = produtoRepository;
        private readonly ProdutoCategoriaRepository _produtoCategoriaRepository = produtoCategoriaRepository;
        private readonly ValidadeRepository _validadeRepository = validadeRepository;
        private readonly MovimentacaoRepository _movimentacaoRepository = movimentacaoRepository;

        public async Task<ResultModel<dynamic>> Add(ProdutoCreateDTO produtoDTO)
        {
            Produto produto = new(codigoBarras: produtoDTO.CodigoBarras, nome: produtoDTO.Nome, quantidade: produtoDTO.Quantidade, quantidadeMinima: produtoDTO.QuantidadeMinima, valor: produtoDTO.Valor, estoqueId: produtoDTO.EstoqueId, ativo: true);
            
            if(await _produtoRepository.AlredyExist(produtoDTO.CodigoBarras))
                return new ResultModel<dynamic>(HttpStatusCode.Conflict, "Já existe um produto com esse código de barras.");
            
            await _produtoRepository.Create(produto);

            ProdutoCategoria produtoCategoria = new(produtoId: produto.Id, categoriaId: produtoDTO.CategoriaId);

            await _produtoCategoriaRepository.Create(produtoCategoria);

            Validade validade = new(produtoId: produto.Id, dataValidade: produtoDTO.DataValidade, quantidade: produtoDTO.Quantidade);
            
            await _validadeRepository.Create(validade);

            Movimentacao movimentacao = new(tipo: "Entrada", produtoId: produto.Id, quantidade: produto.Quantidade, valorTotal: produto.Valor * produto.Quantidade, dataMovimentacao: DateTime.UtcNow);

            await _movimentacaoRepository.Create(movimentacao);


            return new();


        }

        public async Task<ResultModel<dynamic>> Update(int id, ProdutoUpdateDTO produtoDTO)
        {
            Produto produto = await _produtoRepository.GetById(id);
            if(produto == null)
                return new ResultModel<dynamic>(HttpStatusCode.NotFound, "Produto não encontrado.");

            ProdutoCategoria categoria = await _produtoCategoriaRepository.GetByProdutoId(produto.Id);
            
            produto.Nome = produtoDTO.Nome;
            produto.Valor = produtoDTO.Valor;
            produto.QuantidadeMinima = produtoDTO.QuantidadeMinima;
            produto.CodigoBarras = produtoDTO.CodigoBarras;
            produto.EstoqueId = produtoDTO.EstoqueId;
            if (produtoDTO.DataValidade != default(DateOnly))
            {
                if(produtoDTO.Quantidade < 0)
                {
                    produto.Quantidade -= produtoDTO.Quantidade;

                    Movimentacao movimentacao = new(tipo: "Saida", produtoId: produto.Id, quantidade: produtoDTO.Quantidade, valorTotal: produtoDTO.Valor * produtoDTO.Quantidade, dataMovimentacao: DateTime.UtcNow);

                    await _movimentacaoRepository.Create(movimentacao);

                    Validade validade = await _validadeRepository.GetByValidadeAndId(produto.Id, produtoDTO.DataValidade);

                    validade.Quantidade -= produtoDTO.Quantidade;

                    if(produto.Quantidade == 0)
                    {
                        produto.Ativo = false;
                        produto.Excluido = true;
                    }

                    await _validadeRepository.Update(validade);
                }
                else
                {
                    produto.Quantidade += produtoDTO.Quantidade;
                    produto.Ativo = true;
                    produto.Excluido = false;
                    Validade validade = new(produtoId: produto.Id, dataValidade: produtoDTO.DataValidade, quantidade: produtoDTO.Quantidade);
                    await _validadeRepository.Create(validade);
                }
            }

            if (categoria.CategoriaId != produtoDTO.CategoriaId)
            {
                categoria.CategoriaId = produtoDTO.CategoriaId;
            }



            await _produtoRepository.Update(produto);


            if(produtoDTO.Quantidade > 0)
            {
                Movimentacao movimentacao = new(tipo: "Entrada", produtoId: produto.Id, quantidade: produtoDTO.Quantidade, valorTotal: produtoDTO.Valor * produtoDTO.Quantidade, dataMovimentacao: DateTime.UtcNow);
                await _movimentacaoRepository.Create(movimentacao);
            }


            return new();
        }

        public async Task<ResultModel<ProdutoDTO>> GetById(int id)
        {
            Produto produto = await _produtoRepository.GetById(id);
            if (produto == null)
                return new(HttpStatusCode.NotFound, "Produto não encontrado");

            ProdutoCategoria produtoCategoria = await _produtoCategoriaRepository.GetByProdutoId(produto.Id);

            List<Validade> validades =  await _validadeRepository.GetAll(produto.Id);

            ProdutoDTO produtoDTO = new()
            {
                Id = produto.Id,
                CodigoBarras = produto.CodigoBarras,
                Nome = produto.Nome,
                QuantidadeTotal = produto.Quantidade,
                QuantidadeMinima = produto.QuantidadeMinima,
                Valor = produto.Valor,
                EstoqueId = produto.EstoqueId,
                CategoriaNome = produtoCategoria.Categoria.Nome,
                CategoriaId = produtoCategoria.Categoria.Id
            };
            
            List<ValidadeDTO> validadesdto = new List<ValidadeDTO>();
            foreach (Validade validade in validades)
            {
                ValidadeDTO validadeDTO = new ValidadeDTO()
                {
                    Id = validade.Id,
                    DataValidade = validade.DataValidade,
                    Quantidade = validade.Quantidade,
                };
                
                validadesdto.Add(validadeDTO);
            }

            produtoDTO.Validades = validadesdto.OrderByDescending(x => x.DataValidade).ToList();
            
            return new(produtoDTO);
        }

        public async Task<ResultModel<List<ProdutoDTO>>> GetAll(int estoqueId)
        {
            List<Produto> produtos = await _produtoRepository.GetByEstoque(estoqueId);

            if(produtos.Count == 0)
                return new(HttpStatusCode.NotFound, "Nenhum produto deste estoque foi encontrado");

            List <ProdutoDTO> produtosDTO = new();
            List<ValidadeDTO> validadesDTO = new();

            foreach(Produto produto in produtos)
            {
                ProdutoCategoria produtoCategoria = await _produtoCategoriaRepository.GetByProdutoId(produto.Id);
                List<Validade> validades =  await _validadeRepository.GetAll(produto.Id);

                ProdutoDTO produtoDTO = new ProdutoDTO {
                    Id = produto.Id,
                    CodigoBarras = produto.CodigoBarras,
                    Nome = produto.Nome,
                    QuantidadeTotal = produto.Quantidade,
                    QuantidadeMinima = produto.QuantidadeMinima,
                    Valor = produto.Valor,
                    EstoqueId = produto.EstoqueId,
                    CategoriaNome = produtoCategoria.Categoria.Nome,
                    CategoriaId = produtoCategoria.Categoria.Id
                };
                
                produtosDTO.Add(produtoDTO);
                
                foreach (Validade validade in validades)
                {
                    ValidadeDTO validadeDto = new()
                    {
                        Id = validade.Id,
                        DataValidade = validade.DataValidade,
                        Quantidade = validade.Quantidade,
                        

                    };
                    validadesDTO.Add(validadeDto);
                    produtoDTO.Validades = validadesDTO.OrderByDescending(x => x.DataValidade).ToList();
                }
            }

            return new(produtosDTO);
        }
        
        public async Task<ResultModel<ProdutoDTO>> GetByCodBarras(string codBarras)
        {
            Produto produto = await _produtoRepository.GetByCodBarras(codBarras);

            if(produto == null)
                return new(HttpStatusCode.NotFound, "Produto não encontrado");
            
            ProdutoCategoria produtoCategoria = await _produtoCategoriaRepository.GetByProdutoId(produto.Id);

            List<Validade> validades = await _validadeRepository.GetAll(produto.Id);

            ProdutoDTO produtoDTO = new()
            {
                Id = produto.Id,
                CodigoBarras = produto.CodigoBarras,
                Nome = produto.Nome,
                QuantidadeTotal = produto.Quantidade,
                QuantidadeMinima = produto.QuantidadeMinima,
                Valor = produto.Valor,
                EstoqueId = produto.EstoqueId,
                CategoriaNome = produtoCategoria.Categoria.Nome,
                CategoriaId = produtoCategoria.Categoria.Id
            };

            List<ValidadeDTO> validadesdto = new List<ValidadeDTO>();
            foreach (Validade validade in validades)
            {
                ValidadeDTO validadeDTO = new ValidadeDTO()
                {
                    Id = validade.Id,
                    DataValidade = validade.DataValidade,
                    Quantidade = validade.Quantidade,
                };

                validadesdto.Add(validadeDTO);
            }

            produtoDTO.Validades = validadesdto.OrderByDescending(x => x.DataValidade).ToList();

            return new(produtoDTO);
        }

        public async Task<ResultModel<dynamic>> Delete(int id)
        {
            Produto produto = await _produtoRepository.GetById(id);

            if (produto == null) return new(HttpStatusCode.NotFound, "Produto não encontrado");

            await _produtoRepository.Remove(produto);

            Movimentacao movimentacao = new(tipo: "Exclusão", produtoId: produto.Id, quantidade: produto.Quantidade, valorTotal: produto.Valor * produto.Quantidade, dataMovimentacao: DateTime.UtcNow);

            await _movimentacaoRepository.Create(movimentacao);

            return new();
        }

        public async Task<ResultModel<dynamic>> Compra(List<CompraDTO> compraDTO)
        {
            foreach(var item in compraDTO)
            {
                Produto produto = await _produtoRepository.GetById(item.ProdutoId);
                Validade validade = await _validadeRepository.GetByValidadeAndId(item.ProdutoId, item.Validade);

                validade.Quantidade -= item.Quantidade;
                produto.Quantidade -= item.Quantidade;

                await _produtoRepository.Update(produto);
                await _validadeRepository.Update(validade);

                Movimentacao movimentacao = new(tipo: "Saida", produtoId: produto.Id, quantidade: produto.Quantidade, valorTotal: produto.Valor * produto.Quantidade, dataMovimentacao: DateTime.UtcNow);

                await _movimentacaoRepository.Create(movimentacao);

            }

            return new();
        }

        public async Task<ResultModel<List<Produto>>> ListaCompra()
        {
            List<Produto> produtos = await _produtoRepository.ListaCompra();

            if (produtos.Count == 0)
                return new(HttpStatusCode.NotFound, "Nenhum produto para lista de compra");

            return new(produtos);
        }

    }
}
