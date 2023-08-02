using Questao5._3_Domain.Enumerators;

namespace Questao5._3_Domain.Entities
{
    public class ContaCorrente
    {
        public string IdContaCorrente { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public ActiveAccountEnum Ativo { get; set; }
    }
}
