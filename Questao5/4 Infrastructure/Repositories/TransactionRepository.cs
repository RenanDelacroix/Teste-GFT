using Dapper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Data.Sqlite;
using Questao5._3_Domain.DTO;
using Questao5._3_Domain.Entities;
using Questao5._3_Domain.Enumerators;
using Questao5._3_Domain.Interfaces.Repositories;
using Questao5._4_Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Sqlite;
using System.Globalization;
using System.Transactions;

namespace Questao5._4_Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DatabaseConfig _databaseConfig;
        public TransactionRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        public async Task<string> CreateTransaction(string accountId, string type, decimal amount)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var idMovimento = Guid.NewGuid();
            var today = DateTime.Now.ToString("dd/MM/yyyy");
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var query = TransactionQueries.CreateQuery(idMovimento, accountId, today, type, amount);
            var rows = await connection.ExecuteAsync(query,
                new { idMovimento, accountId, today, type, amount });

            if (rows >= 1)
                return idMovimento.ToString();

            return string.Empty;

        }

        public async Task<BalanceDTO> GetBalance(string accountId)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var transacts = await connection.QueryAsync<Movimento>(TransactionQueries.GetQuery(accountId));
            var saldo = transacts.Where(y => y.Tipomovimento == TransactionType.C.ToString()).Sum(x => x.Valor) - transacts.Where(y => y.Tipomovimento == TransactionType.D.ToString()).Sum(x => x.Valor);
            var response = new BalanceDTO
            {
                Data = DateTime.Now.ToString("dd/MM/yyyy - HH:mm"),
                Saldo = Math.Round(saldo, 2).ToString("0.00"),
            };
            return response;

        }
    }
}
