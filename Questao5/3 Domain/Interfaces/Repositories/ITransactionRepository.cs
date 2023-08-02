using Questao5._3_Domain.DTO;
using Questao5._3_Domain.Entities;

namespace Questao5._3_Domain.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<string> CreateTransaction(string accountId, string type, decimal amount);
        Task<BalanceDTO> GetBalance(string accountId);
    }
}
