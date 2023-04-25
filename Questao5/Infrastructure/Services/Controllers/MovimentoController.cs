using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.Interfaces;
using System.Net;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/movimentacao")]
    [ApiController]
    public class MovimentoController : Controller
    {
        protected IMediator MediatorService { get; }
        protected IContaCorrenteRepository _contaCorrenteRepository { get; }

        public MovimentoController(IMediator mediatorService, IContaCorrenteRepository contaCorrenteRepository)
        {
            MediatorService = mediatorService;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        /// <summary>
        /// Realiza a movimentação de contas, com valores do tipo Crédito ou Débito, TipoMovimento pode ser 0 = Debito e 1 = Credito 
        /// </summary>
        /// <param name="movimentacaoQuery"></param>
        /// <returns></returns>
        [HttpPost("inserir")]
        [ProducesResponseType(typeof(AddMovimentacaoQueryResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddMovimentacaoAsync(AddMovimentacaoQuery movimentacaoQuery)
        {
            GetContaCorrenteQueryResponse contaCorrente = await MediatorService.Send(new GetContaCorrenteQuery() { idcontacorrente = movimentacaoQuery.IdContaCorrente });
            //GetContaCorrenteQueryResponse contaCorrente = await _contaCorrenteRepository.GetContaCorrentePorId(new GetContaCorrenteQuery() { idcontacorrente = movimentacaoQuery.IdContaCorrente });

            if (contaCorrente == null || contaCorrente?.idcontacorrente == null)
                return BadRequest(new ErrorQueryResponse() { Erro = "Apenas contas correntes cadastradas podem receber movimentação", TipoFalha = "INVALID_ACCOUNT" });
            if (!contaCorrente.ativo)
                return BadRequest(new ErrorQueryResponse() { Erro = "Apenas contas correntes ativas podem receber movimentação", TipoFalha = "INACTIVE_ACCOUNT" });
            if (!(movimentacaoQuery.TipoMovimentacao == TipoMovimento.Debit || movimentacaoQuery.TipoMovimentacao == TipoMovimento.Credit))
                return BadRequest(new ErrorQueryResponse() { Erro = "Apenas os tipos 0 = Debito e 1 = Credito podem ser aceitos em TipoMovimento", TipoFalha = "INVALID_TYPE" });
            if (movimentacaoQuery.Valor < 0)
                return BadRequest(new ErrorQueryResponse() { Erro = "Apenas valores positivos podem ser recebidos no Valor", TipoFalha = "INVALID_VALUE" });

            var response = await MediatorService.Send(movimentacaoQuery);

            if (response == null) 
                return BadRequest(new ErrorQueryResponse() { Erro = "Ocorreu um erro desconhecido", TipoFalha = "UNKNOWN_ERROR" });

            return Ok(response);
        }

    }
}
