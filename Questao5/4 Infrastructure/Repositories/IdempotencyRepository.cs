using Dapper;
using Microsoft.Data.Sqlite;
using Questao5._3_Domain.Entities;
using Questao5._3_Domain.Interfaces.Repositories;
using Questao5._4_Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Sqlite;

namespace Questao5._4_Infrastructure.Repositories
{
    public class IdempotencyRepository : IIdempotencyRepository
    {
        private readonly DatabaseConfig _databaseConfig;
        public IdempotencyRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        public async Task CreateRegister(string key, string request)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var rows = await connection.ExecuteAsync(IdempotencyQueries.CreateRegister(key, request));
        }

        public async Task<Idempotencia> GetRegister(string key)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var result = await connection.QueryAsync<Idempotencia>(IdempotencyQueries.GetByKey(key));
            return result.FirstOrDefault();

        }

        public async Task UpdateRegisterResult(string key, string result)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var response = await connection.ExecuteAsync(IdempotencyQueries.UpdateRegister(key, result));
        }

    }
}
