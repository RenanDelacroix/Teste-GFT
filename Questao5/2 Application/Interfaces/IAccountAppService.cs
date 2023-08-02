using Questao5._2_Application.Commands.Requests;
using Questao5._3_Domain.DTO;
using Questao5._3_Domain.Enumerators;

namespace Questao5._2_Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<ResultDTO<BalanceDTO>> GetAccountBalance(string accountId);
        Task<ResultDTO<dynamic>> Transaction(string accountId, Guid idempotency_key, TransactionType transaction, decimal amount);
    }
}
