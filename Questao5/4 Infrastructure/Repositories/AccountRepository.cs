using Dapper;
using Microsoft.Data.Sqlite;
using Questao5._3_Domain.Entities;
using Questao5._3_Domain.Interfaces.Repositories;
using Questao5._4_Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Sqlite;

namespace Questao5._4_Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseConfig _databaseConfig;
        public AccountRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        public async Task<ContaCorrente> GetAccountById(string id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var result = await connection.QueryAsync<ContaCorrente>(GetAccountByIdQuery.ReturnQuery(id));
            return result.FirstOrDefault();

        }
    }
}
