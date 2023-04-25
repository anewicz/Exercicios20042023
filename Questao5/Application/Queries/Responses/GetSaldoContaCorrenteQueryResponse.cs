using MediatR;
using Newtonsoft.Json;
using Questao5.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Questao5.Application.Queries
{
    public class GetSaldoContaCorrenteQueryResponse 
    {
        [JsonProperty("NumeroConta")]
        public int numero { get; set; }
        [JsonProperty("Nome")]
        public string nome { get; set; }

        [JsonProperty("ValorSaldo")]
        public double valorSaldo { get; set; }

        [JsonProperty("DataConsulta")]
        public string dataConsulta { get; set; }
    }
}
