using Newtonsoft.Json;
using Questao2.DTOs;
using RestSharp;

namespace Questao2
{
    class MatchResultsService
    {
        private string urlTeams = "https://jsonmock.hackerrank.com/api/football_matches";
        private RestClient client;
        public MatchResultsService()
        {
            client = new RestClient(urlTeams);
        }

        public string GetTeamScores(int year, string team1)
        {
            var result = GetMatchesData(year, team1);
            return result.ResultsFound ? $"Team {result.Team} scored {result.ScoredGoals} in {result.Year}" : "No results found";
        }

        private ScoresDTO GetMatchesData(int year, string team1)
        {
            IList<MatchesDTO> matches = new List<MatchesDTO>();
            int count = 1;
            int totalPages = 0;
            urlTeams += $"?year={year}&team1={team1}&page={count}";

            do
            {
                RestRequest request = GetRestRequest(year, team1, count);
                var response = client.Execute(request);
                if (response == null || response?.Content == null)
                    return new ScoresDTO(false);

                var matchResult = JsonConvert.DeserializeObject<MatchesDTO>(response.Content);
                matches.Add(matchResult);
                if (matchResult.TotalPages > 1 && totalPages == 0)
                    totalPages = matchResult.TotalPages;

                count++;
                if (totalPages > 0)
                    urlTeams = urlTeams.Replace($"page={count - 1}", $"page={count}");

            } while (count <= totalPages);

            return ComputeScores(matches, team1, year);
        }

        private RestRequest GetRestRequest(int year, string team1, int count)
        {
            var request = new RestRequest(urlTeams, Method.Get);
            return request.AddParameter("text/plain", "", ParameterType.RequestBody);
        }

        private ScoresDTO ComputeScores(IList<MatchesDTO> matches, string team, int year)
        {
            var scores = new ScoresDTO(team, year);
            foreach (var match in matches)
                scores.ScoredGoals += match.Data.Sum(x => x.Team1Goals);

            return scores;
        }
    }
}
