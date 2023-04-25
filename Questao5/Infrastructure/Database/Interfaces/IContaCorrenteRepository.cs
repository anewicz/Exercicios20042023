using FluentAssertions.Equivalency;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Interfaces
{
    public interface IContaCorrenteRepository
    {
        Task<GetContaCorrenteQueryResponse> GetContaCorrentePorId(GetContaCorrenteQuery request);
        Task<GetSaldoContaCorrenteQueryResponse> GetSaldoContaCorrentePorId(GetSaldoContaCorrenteQuery request);

    }

}
