using System.ComponentModel;

namespace Questao2.DTOs
{
    public class ScoresDTO
    {
        public ScoresDTO(string team, int year)
        {
            this.Team = team;
            this.Year = year;
            this.ResultsFound = true;
        }
        public ScoresDTO(bool resultsFound) 
        {
            this.ResultsFound = resultsFound;
        }
        public string Team { get; set; }
        public int ScoredGoals { get; set; }
        public int Year { get; set; }

        [DefaultValue(true)]
        public bool ResultsFound { get; set; }
    }   
}
