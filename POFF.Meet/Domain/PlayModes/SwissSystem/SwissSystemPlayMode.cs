using POFF.Meet.Domain.PlayModes;
using POFF.Meet.View.Model;
using System.Collections.Generic;
using System.Linq;

namespace POFF.Meet;

public class SwissSystemPlayMode(in Tournament tournament) : PlayMode("Swiss System")
{
    private readonly Tournament _tournament = tournament;

    public override bool IsIterative => true;

    public override IEnumerable<Fixture> Generate(int teamsCount, int round = 0)
    {
        int matchesPerRound = teamsCount / 2;
        bool isPreviousRoundFinished = round == 1 ||
            matchesPerRound == _tournament.Matches.Count(
            m => m.Section == round - 1 && m.Status == Domain.MatchStatus.Finished);

        if (round == 0 || teamsCount % 2 != 0 || !isPreviousRoundFinished)
            yield break;

        // For subsequent rounds pair players with the same score against each other.
        var teamsList = _tournament.Teams.ToList();
        var standings = _tournament.GetStandings().ToList();

        // Group standings by score (descending) preserving order within same score group
        var groups = standings
            .GroupBy(s => s.Score)
            .OrderByDescending(g => g.Key)
            .Select(g => g.Select(s => teamsList.IndexOf(s.Team)).ToList())
            .ToList();

        var fixturesYielded = 0;

        for (int g = 0; g < groups.Count && fixturesYielded < matchesPerRound; g++)
        {
            var group = groups[g];

            int i = 0;
            while (i + 1 < group.Count && fixturesYielded < matchesPerRound)
            {
                yield return new Fixture(group[i], group[i + 1]) { Round = round };
                fixturesYielded++;
                i += 2;
            }

            // If there's an unpaired player in this group, try to pair with first of next group
            if (i < group.Count && fixturesYielded < matchesPerRound)
            {
                // find next group that has at least one available player
                int nextGroupIndex = g + 1;
                while (nextGroupIndex < groups.Count && groups[nextGroupIndex].Count == 0)
                    nextGroupIndex++;

                if (nextGroupIndex < groups.Count)
                {
                    var opponent = groups[nextGroupIndex][0];
                    yield return new Fixture(group[i], opponent) { Round = round };
                    fixturesYielded++;
                    // remove that opponent from its group
                    groups[nextGroupIndex].RemoveAt(0);
                }
            }
        }
    }
}