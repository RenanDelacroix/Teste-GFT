using Questao5._3_Domain.Entities;

namespace Questao5._3_Domain.Interfaces.Repositories
{
    public interface IIdempotencyRepository
    {
        Task CreateRegister(string key, string request);
        Task<Idempotencia> GetRegister(string key);
        Task UpdateRegisterResult(string key, string result);
    }
}
