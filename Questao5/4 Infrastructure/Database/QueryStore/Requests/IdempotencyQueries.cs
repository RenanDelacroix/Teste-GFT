namespace Questao5._4_Infrastructure.Database.QueryStore.Requests
{
    public static class IdempotencyQueries
    {
        public static string GetByKey(string key)
            => $"SELECT chave_idempotencia Chaveidempotencia, requisicao, resultado FROM idempotencia WHERE chave_idempotencia = '{key}' ";

        public static string CreateRegister(string key, string request)
            => $"INSERT INTO idempotencia (chave_idempotencia, requisicao ) " +
               $"VALUES ('{key}', '{request}') ";

        public static string UpdateRegister(string key, string result)
            => $"UPDATE idempotencia SET resultado = '{result}' " +
               $"WHERE chave_idempotencia = '{key}' ";
    }
}
