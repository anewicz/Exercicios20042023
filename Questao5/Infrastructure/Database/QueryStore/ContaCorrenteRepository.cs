using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {

        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<GetContaCorrenteQueryResponse> GetContaCorrentePorId(GetContaCorrenteQuery request)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            string query = @$"SELECT * 
                              FROM contacorrente
                              WHERE idcontacorrente = '{request.idcontacorrente.ToString().ToUpper()}'";                

            var result = connection.QueryAsync<GetContaCorrenteQueryResponse>(query).Result?.FirstOrDefault();

            return result;

        }

        public async Task<GetSaldoContaCorrenteQueryResponse> GetSaldoContaCorrentePorId(GetSaldoContaCorrenteQuery request)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);
            string query = @$"SELECT 
                              c.numero,
                              c.nome,
                              DATETIME() 'dataConsulta',
                              (SELECT SUM(COALESCE(valor, 0)) FROM movimento WHERE idcontacorrente = c.idcontacorrente AND tipomovimento = 'C')
                              - (SELECT SUM(COALESCE(valor, 0)) FROM movimento WHERE idcontacorrente = c.idcontacorrente AND tipomovimento = 'D') AS valorSaldo
                              FROM contacorrente c
                              WHERE c.idcontacorrente = '{request.idcontacorrente.ToString().ToUpper()}'
                              AND c.ativo = 1";

            var result = connection.QueryAsync<GetSaldoContaCorrenteQueryResponse>(query).Result?.FirstOrDefault();

            return result;

        }        

    }
}
