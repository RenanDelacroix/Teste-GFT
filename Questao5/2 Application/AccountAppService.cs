using Newtonsoft.Json;
using Questao5._2_Application.Interfaces;
using Questao5._3_Domain.DTO;
using Questao5._3_Domain.Entities;
using Questao5._3_Domain.Enumerators;
using Questao5._3_Domain.Interfaces.Repositories;

namespace Questao5._2_Application
{
    public class AccountAppService : IAccountAppService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IIdempotencyRepository _idemPotencyRepository;
        private readonly IAccountRepository _accountRepository;
        public AccountAppService(ITransactionRepository transactionRepository, IIdempotencyRepository idemPotencyRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _idemPotencyRepository = idemPotencyRepository;
            _accountRepository = accountRepository;
        }
        public async Task<ResultDTO<BalanceDTO>> GetAccountBalance(string accountId)
        {
            var validAccount = await ValidateAccount(accountId);
            if (validAccount == null)
                return new ResultDTO<BalanceDTO> { Success = false, Message = "Conta Não Existe" };

            if (validAccount.Ativo == ActiveAccountEnum.INACTIVE_ACCOUNT)
                return new ResultDTO<BalanceDTO> { Success = false, Message = "Conta Inativa" };

            var result = await _transactionRepository.GetBalance(accountId);
            result.NumeroConta = validAccount.Numero.ToString();
            result.NomeTitular = validAccount.Nome;
            return new ResultDTO<BalanceDTO> { Success = true, Message = "Consulta Saldo", Data = result };
        }

        public async Task<ResultDTO<dynamic>> Transaction(string accountId, Guid idempotency_key, TransactionType transaction, decimal amount)
        {
            var idempotencyRunning = await CheckIdempotencyRegister(idempotency_key);
            if(idempotencyRunning != null)
                return new ResultDTO<dynamic> { Success = true, Message = "Transação efetuada", Data = idempotencyRunning };
            else
                await CreateIdempotencyRegister(idempotency_key, accountId, transaction, amount);

            var validAccount = await ValidateAccount(accountId);
            if (validAccount == null)
                return new ResultDTO<dynamic> { Success = false, Message = "Conta Não Existe" };

            if (validAccount.Ativo == ActiveAccountEnum.INACTIVE_ACCOUNT)
                return new ResultDTO<dynamic> { Success = false, Message = "Conta Inativa" };

            var result = await _transactionRepository.CreateTransaction(accountId, transaction.ToString(), amount);

            if (string.IsNullOrEmpty(result))
                return new ResultDTO<dynamic> { Success = false, Message = "Error Null Response Transaction" };

            await UpdateIdempotencyRegister(idempotency_key, result);

            return new ResultDTO<dynamic> { Success = true, Message = "Transação efetuada", Data = result};
        }

        private async Task<ContaCorrente> ValidateAccount(string accountId)
        {
            var account =  await _accountRepository.GetAccountById(accountId);
            return account;
        }

        private async Task<Idempotencia> CheckIdempotencyRegister(Guid key)
        {
            if (key == Guid.Empty)
                return null;

            var idempotency = await _idemPotencyRepository.GetRegister(key.ToString());
            if(idempotency == null)
                return null;

            return idempotency;
        }

        private async Task CreateIdempotencyRegister(Guid key, string accountId, TransactionType transaction, decimal amount)
        {
            var request = JsonConvert.SerializeObject(new { Transaction = transaction.ToString(), Amount = amount, AccountId = accountId });
            var jsonObject = request.Replace("\\\"", "").Replace("\\\"", "\"");
            await _idemPotencyRepository.CreateRegister(key.ToString(), jsonObject);
        }
        private async Task UpdateIdempotencyRegister(Guid key, string result)
        {
            var idempotency = await _idemPotencyRepository.GetRegister(key.ToString());
            idempotency.Resultado = result;
            await _idemPotencyRepository.UpdateRegisterResult(key.ToString(), JsonConvert.SerializeObject(idempotency).Replace("\\\"", "\""));
        }
    }
}
