using CoachBoard.Core.Helpers.InitialTeamSetup;
using Newtonsoft.Json;

namespace CoachBoard.Core.Extensions;

public static class InitialTeamExtensions
{
    public static InitialTeam? GenerateInitialTeam(string teamName)
    {
        try
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Helpers/InitialTeamSetup/Data/{teamName}.json");
            using StreamReader r = new(path);
            var json = r.ReadToEnd();
            r.Dispose();
            return JsonConvert.DeserializeObject<InitialTeam>(json);
        }
        catch
        {
            return null;
        }
    }
}