using Newtonsoft.Json.Converters;
using Questao5._3_Domain.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Questao5._2_Application.Commands.Requests
{
    public class TransactionTypeCommand
    {
        
        public TransactionType TransactionType { get; set; }
    }
}
