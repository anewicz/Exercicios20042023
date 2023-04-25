using Newtonsoft.Json;
using Questao2;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Dynamic;
using System.Runtime.CompilerServices;

public class Program
{
    public static async Task Main(string[] args)
    {

        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoalsAsync(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoalsAsync(string team, int year)
    {
        int goals = 0;
        try
        {

            for (var teamPosition = 1; teamPosition <= 2;)
            {
                var resultTeam = await GetRoot(team, year, teamPosition, 1);
                for (var page = 1; page <= resultTeam?.total_pages;)
                {
                    foreach (var teamGols in resultTeam.data)
                    {
                        if (teamPosition == 1)
                            goals += int.Parse(teamGols.team1goals);
                        else
                            goals += int.Parse(teamGols.team2goals);
                    }

                    page++;
                    resultTeam = await GetRoot(team, year, teamPosition, page);
                }
                teamPosition++;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro ao conectar na Api: " + ex.Message);
        }
        return goals;
    }

    private static async Task<Root?> GetRoot(string team, int year, int teamPosition, int page)
    {
        Root? result = new Root();
        try
        {
            var client = new HttpClient();
            var responseTeam = await client.GetAsync($"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team{teamPosition}={team}&page={page}");
            if (responseTeam.IsSuccessStatusCode)
            {
                var jsonString = await responseTeam.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Root>(jsonString);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro: " + ex.Message);
        }

        return result;
    }
}