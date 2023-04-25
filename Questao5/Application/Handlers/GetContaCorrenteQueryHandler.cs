using MediatR;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Application.Handlers
{
    public class GetContaCorrenteQueryHandler : IRequestHandler<GetContaCorrenteQuery, GetContaCorrenteQueryResponse>
    {
        private readonly IContaCorrenteRepository _repository;

        public GetContaCorrenteQueryHandler(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetContaCorrenteQueryResponse> Handle(GetContaCorrenteQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetContaCorrentePorId(request);
        }
    }
}
