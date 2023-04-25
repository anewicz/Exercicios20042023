using MediatR;
using Newtonsoft.Json;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using System.Diagnostics.CodeAnalysis;

namespace Questao5.Application.Queries
{
    public class AddMovimentacaoQueryResponse
    {
        [JsonProperty("IdMovimento")]
        public string IdMovimento { get; set; }
    }
}
