namespace Questao5._4_Infrastructure.Database.QueryStore.Requests
{
    public static class GetAccountByIdQuery
    {
        public static string ReturnQuery(string id)
            => $"SELECT idcontacorrente, numero, nome, ativo FROM contacorrente WHERE idcontacorrente = '{id}' ";
    }
}
