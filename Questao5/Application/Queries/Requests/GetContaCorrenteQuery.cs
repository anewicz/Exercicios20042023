using MediatR;
using Newtonsoft.Json;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries
{
    public class GetContaCorrenteQuery : IRequest<GetContaCorrenteQueryResponse>
    {
        [JsonProperty("IdContaCorrente")]
        public string idcontacorrente { get; set; }

    }
}
