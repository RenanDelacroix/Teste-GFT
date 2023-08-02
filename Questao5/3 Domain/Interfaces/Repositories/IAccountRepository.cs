using Questao5._3_Domain.Entities;

namespace Questao5._3_Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<ContaCorrente> GetAccountById(string id);
    }
}
