using System.Collections.Generic;
using System.Linq;

namespace POFF.Meet.Domain.PlayModes.Matchdays;

public class MatchdaysPlayMode : PlayMode
{
    public MatchdaysPlayMode() : base("Match Days")
    { }

    public override IEnumerable<Fixture> Generate(int teamsCount, int round = 0)
    {
        // Only even number of teams except 6 can be distributed in matchdays
        if (teamsCount % 2 != 0 || teamsCount == 6) return [];

        var matchdays = GenerateMatchdays(teamsCount);

        return MatchIndexPairsOrderdByMatchdays(matchdays);
    }

    private static IEnumerable<Fixture> MatchIndexPairsOrderdByMatchdays(IEnumerable<Matchday> matchdays)
    {
        int section = 0;
        foreach (var matchday in matchdays)
        {
            section++;
            foreach (var fixture in matchday)
            {
                fixture.Round = section;
                yield return fixture;
            }
        }
    }

    private static IEnumerable<Matchday> GenerateMatchdays(int teamsCount)
    {
        var matches = new List<Fixture>(GenerateMatches(teamsCount));
        int matchCountPerMatchDay = teamsCount / 2;
        int matchDaysCount = teamsCount - 1;

        for (int i = 1; i <= matchDaysCount; i++)
        {
            yield return GenerateMatchday(matches, matchCountPerMatchDay);
        }
    }

    private static IEnumerable<Fixture> GenerateMatches(int teamsCount)
    {
        for (int i = 1, loopTo = teamsCount; i <= loopTo; i++)
        {
            for (int j = i + 1, loopTo1 = teamsCount; j <= loopTo1; j++)
            {
                yield return new Fixture(i - 1, j - 1);
            }
        }
    }

    private static Matchday GenerateMatchday(List<Fixture> matches, int matchCountPerMatchDay)
    {
        var matchday = new Matchday();
        while (matchday.Count < matchCountPerMatchDay)
        {
            var invalidatedMatches = new List<Fixture>();
            var nextMatch = GetNextMatch(matches, matchday);
            if (nextMatch.Equals(Fixture.Empty))
            {
                nextMatch = matches.Except(invalidatedMatches).FirstOrDefault() ?? Fixture.Empty;
                invalidatedMatches.AddRange(matchday.MatchesWithPlayersFrom(nextMatch));
                matches.AddRange(invalidatedMatches);
                matchday.RemoveAll(m => invalidatedMatches.Contains(m));
            }
            matchday.Add(nextMatch);
            matches.Remove(nextMatch);
        }
        return matchday;
    }

    private static Fixture GetNextMatch(IEnumerable<Fixture> fixtures, Matchday matchday)
    {
        if (!fixtures.Any()) return Fixture.Empty;

        foreach (var fixture in fixtures)
        {
            if (!matchday.ContainsPlayerIn(fixture))
            {
                return fixture;
            }
        }

        return Fixture.Empty;
    }
}