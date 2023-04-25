using Newtonsoft.Json;
using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        [JsonProperty("IdMovimento")]        
        public string idmovimento { get; set; }
        [JsonProperty("IdContaCorrente")]
        public string idcontacorrente { get; set; }
        [JsonProperty("DataMovimento")]
        public string datamovimento { get; set; }
        [JsonProperty("TipoMovimento")]
        public TipoMovimento tipomovimento { get; set; }
        [JsonProperty("Valor")]
        public double valor { get; set; }
    }
}
