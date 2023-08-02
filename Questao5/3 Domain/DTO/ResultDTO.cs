namespace Questao5._3_Domain.DTO
{
    public class ResultDTO<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
    }
}
