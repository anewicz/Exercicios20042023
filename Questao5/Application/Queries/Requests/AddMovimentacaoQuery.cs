using MediatR;
using Newtonsoft.Json;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Queries
{
    public class AddMovimentacaoQuery : IRequest<AddMovimentacaoQueryResponse>
    {
        [JsonProperty("IdContaCorrente")]
        public string IdContaCorrente { get; set; }

        [JsonProperty("TipoMovimentacao")]
        public TipoMovimento TipoMovimentacao { get; set; }

        [JsonProperty("Valor")]
        public double Valor { get; set; }
    }
}
