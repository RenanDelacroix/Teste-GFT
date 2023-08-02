using Newtonsoft.Json;

namespace Questao2.DTOs
{
    public class MatchesDTO
    {
        public int Page { get; set; }
        [JsonProperty("per_page")]
        public int PerPage { get; set; }
        public int Total { get; set; }
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
        public List<MatchesDataDTO> Data { get; set; }
    }

    public class MatchesDataDTO
    {
        public string Competition { get; set; }
        public int Year { get; set; }
        public string Round { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int Team1Goals { get; set; }
        public int Team2Goals { get; set; }
    }
}
