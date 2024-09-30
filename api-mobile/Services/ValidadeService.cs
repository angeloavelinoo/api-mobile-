using api_mobile.DTOs;
using api_mobile.Model;
using api_mobile.Repository;
using api_mobile.ViewModel;
using System.Net;

namespace api_mobile.Services
{
    public class ValidadeService(ValidadeRepository validadeRepository)
    {
        private readonly ValidadeRepository _validadeRepository = validadeRepository;


        public async Task<ResultModel<List<ValidadeDTO>>> GetAll(int produtoId)
        {
           List<Validade> validades  =  await _validadeRepository.GetAll(produtoId);
            if (validades == null)
                return new ResultModel<List<ValidadeDTO>>(HttpStatusCode.NotFound, "Nenhuma validade encontrada");

            List<ValidadeDTO> validadesDTO = new();

            foreach (Validade validade in validades)
            {
                validadesDTO.Add(new()
                {
                    Id = validade.Id,
                    DataValidade = validade.DataValidade,
                    Quantidade = validade.Quantidade
                });
            }

            return new(validadesDTO);
        }
    }
}
