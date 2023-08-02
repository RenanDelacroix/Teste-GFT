using System.Text.Json.Serialization;

namespace Questao5._3_Domain.Enumerators
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionType
    {
        C,
        D
    }
}
