using MediatR;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Application.Handlers
{
    public class GetSaldoContaCorrenteQueryHandler : IRequestHandler<GetSaldoContaCorrenteQuery, GetSaldoContaCorrenteQueryResponse>
    {
        private readonly IContaCorrenteRepository _repository;

        public GetSaldoContaCorrenteQueryHandler(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetSaldoContaCorrenteQueryResponse> Handle(GetSaldoContaCorrenteQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetSaldoContaCorrentePorId(request);
        }
    }
}
