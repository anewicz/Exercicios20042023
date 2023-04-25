using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Sqlite;
using System.Drawing;
using System.Globalization;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {

        private readonly DatabaseConfig databaseConfig;

        public MovimentoRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<AddMovimentacaoQueryResponse> AddMovimentacao(AddMovimentacaoQuery request)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var idMovimento = $"{Guid.NewGuid()}";
            var parameters = new
            {
                idmovimento = idMovimento,
                idcontacorrente = request.IdContaCorrente,
                datamovimento = DateTime.Now,
                tipomovimento = (request.TipoMovimentacao == TipoMovimento.Debit) ? 'D' : 'C',
                valor = request.Valor.ToString("###.##", new CultureInfo("en-US"))
            };


            string query = $@"INSERT INTO movimento 
                            ( idmovimento
                            , idcontacorrente
                            , datamovimento
                            , tipomovimento
                            , valor )
                              VALUES
                            ( '{parameters.idmovimento}'
                            , '{parameters.idcontacorrente}'
                            , '{parameters.datamovimento}'
                            , '{parameters.tipomovimento}'
                            ,  {parameters.valor} )";

            try
            {
                connection.ExecuteScalar(query);
                return new AddMovimentacaoQueryResponse() { IdMovimento = idMovimento };
            }
            catch (Exception ex)
            {
                return new AddMovimentacaoQueryResponse();
            }
        }
    }
}



