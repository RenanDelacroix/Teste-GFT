using Questao5._3_Domain.Enumerators;

namespace Questao5._4_Infrastructure.Database.QueryStore.Requests
{
    public static class TransactionQueries
    {
        public static string CreateQuery(Guid idmovimento, string accountId, string today, string type, decimal amount)
            => $"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) " +
                $"VALUES ('{idmovimento}', '{accountId}', '{today}', '{type}', {Math.Round(amount, 2)}) ";

        public static string GetQuery(string accountId)
            => $"SELECT idmovimento, idcontacorrente, datamovimento, tipomovimento, valor FROM movimento WHERE idcontacorrente = '{accountId}' ";


    }
}
