using FluentAssertions.Equivalency;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Interfaces
{
    public interface IMovimentoRepository
    {
        Task<AddMovimentacaoQueryResponse> AddMovimentacao(AddMovimentacaoQuery request);

    }

}
