using Newtonsoft.Json;

namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {

        [JsonProperty("IdContaCorrente")]
        public string idcontacorrente { get; set; }
        [JsonProperty("NumeroConta")]
        public int numero { get; set; }
        [JsonProperty("Nome")]
        public string nome { get; set; }
        [JsonProperty("Ativo")]
        public bool ativo { get; set; }
    }
}
