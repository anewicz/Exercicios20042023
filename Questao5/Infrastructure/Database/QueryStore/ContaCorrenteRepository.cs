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
                              WHERE idcontacorrente = '{request.idcontacorrente}'";                

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
                            COALESCE(SUM(m.valor_credito), 0) - COALESCE(SUM(m.valor_debito), 0) AS valorSaldo
                            FROM contacorrente c
                            LEFT JOIN (
                            SELECT 
                                idcontacorrente,
                                SUM(CASE WHEN tipomovimento = 'C' THEN COALESCE(valor, 0) ELSE 0 END) AS valor_credito,
                                SUM(CASE WHEN tipomovimento = 'D' THEN COALESCE(valor, 0) ELSE 0 END) AS valor_debito
                            FROM movimento
                            GROUP BY idcontacorrente
                            ) m ON c.idcontacorrente = m.idcontacorrente
                            WHERE c.idcontacorrente = '{request.idcontacorrente}'
                            AND c.ativo = 1
                            GROUP BY c.numero, c.nome ";

            var result = connection.QueryAsync<GetSaldoContaCorrenteQueryResponse>(query).Result?.FirstOrDefault();

            return result;

        }        

    }
}
