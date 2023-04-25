using Newtonsoft.Json;

namespace Questao5.Domain.Entities
{
    public class Idempotencia
    {
        [JsonProperty("Chave_Idempotencia")]
        public Guid chave_idempotencia { get; set; }
        [JsonProperty("Requisicao")]
        public string requisicao { get; set; }
        [JsonProperty("Resultado")]
        public string resultado { get; set; }
    }
}
