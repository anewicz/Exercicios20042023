using MediatR;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Application.Handlers
{
    public class AddMovimentacaoQueryHandler : IRequestHandler<AddMovimentacaoQuery, AddMovimentacaoQueryResponse>
    {
        private readonly IMovimentoRepository _repository;

        public AddMovimentacaoQueryHandler(IMovimentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<AddMovimentacaoQueryResponse> Handle(AddMovimentacaoQuery request, CancellationToken cancellationToken)
        {
            return await _repository.AddMovimentacao(request);
        }
    }
}
