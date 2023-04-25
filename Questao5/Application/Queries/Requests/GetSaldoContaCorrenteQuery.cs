﻿using MediatR;
using Newtonsoft.Json;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries
{
    public class GetSaldoContaCorrenteQuery : IRequest<GetSaldoContaCorrenteQueryResponse>
    {
        [JsonProperty("IdContaCorrente")]
        public string idcontacorrente { get; set; }

    }
}
