using Questao2;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        Console.WriteLine();
        Console.WriteLine(getTotalScoredGoals(teamName, year));

        teamName = "Chelsea";
        year = 2014;

        Console.WriteLine(getTotalScoredGoals(teamName, year));

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
        
        /* Observação
         * O número de gols marcados está diferente do "Output expected", 
         * mas é o que a API proposta está oferecendo mesmo, no momento. 
         * As somas batem com o resultado da API.
         */
    }

    public static string getTotalScoredGoals(string team, int year)
    {
        var resultGoals = new MatchResultsService().GetTeamScores(year, team);
        return resultGoals;
    }

}