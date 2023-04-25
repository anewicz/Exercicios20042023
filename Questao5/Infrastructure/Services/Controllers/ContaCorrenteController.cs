using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.Repositories;
using System.Net;

namespace Questao5.Infrastructure.Services.Controllers
{

    [Route("api/contacorrente")]
    [ApiController]
    public class ContaCorrenteController : Controller
    {
        protected IMediator MediatorService { get; }

        public ContaCorrenteController(IMediator mediatorService)
        {
            MediatorService = mediatorService;
        }

        /// <summary>
        /// Realiza a consulta de saldo da conta corrente pelo Id da conta corrente ex: FA99D033-7067-ED11-96C6-7C5DFA4A16C9
        /// </summary>
        /// <param name="contaCorrenteId"></param>
        /// <returns></returns>
        [HttpGet("consultarsaldo/{contaCorrenteId}")]
        [ProducesResponseType(typeof(GetSaldoContaCorrenteQuery), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSaldoContaCorrentePorIdAsync(string contaCorrenteId)
        {
            GetContaCorrenteQueryResponse contaCorrente = await MediatorService.Send(new GetContaCorrenteQuery() { idcontacorrente = contaCorrenteId });

            if (contaCorrente == null || contaCorrente?.idcontacorrente == null)
                return BadRequest(new ErrorQueryResponse() { Erro = "Apenas contas correntes cadastradas podem consultar o saldo", TipoFalha = "INVALID_ACCOUNT" });
            if (!contaCorrente.ativo)
                return BadRequest(new ErrorQueryResponse() { Erro = "Apenas contas correntes ativas podem consultar o saldo", TipoFalha = "INACTIVE_ACCOUNT" });

            var response = await MediatorService.Send(new GetSaldoContaCorrenteQuery() { idcontacorrente = contaCorrenteId });

            return Ok(response);
        }

    }
}
