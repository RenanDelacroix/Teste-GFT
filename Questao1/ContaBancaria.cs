using System.Runtime.InteropServices;

namespace Questao1
{
    class ContaBancaria {

        const double taxaSaque = 3.50;
        private bool contaCriada = false; 
        public ContaBancaria(int numero, string titular, [Optional]double? depositoInicial)
        {
            this.CriarConta(numero, titular, depositoInicial == null ? 0 : depositoInicial);

        }

        //Propriedade com mecanismo de proteção para evitar alteração de número de conta criada.
        private int numero;
        public int GetNumero()
        {
            return numero;
        }
        private void SetNumero(int value)
        {
            if(!contaCriada)
            {
                numero = value;
            }
        }

        public string  Titular { get; private set; }
        public double? DepositoInicial { get; private set; }
        public double Saldo { get; private set; }

        public void Deposito(double quantia)
        {
            Saldo += quantia;
        }

        public void Saque(double quantia)
        {
            Saldo -= (quantia + taxaSaque);
        }

        private void CriarConta(int numero, string titular, double? depositoInicial) 
        {
            if (contaCriada) //Mecanismo de proteção para conta criada
                return;

            SetNumero(numero);
            Titular = titular.Trim();
            DepositoInicial = depositoInicial;
            this.Deposito(depositoInicial.Value);
            contaCriada = true;
        }
        private void AlterarNome(string titular)
        {
            Titular = titular.Trim();
        }
    }
}
