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

        for (int i = 1; i < teamsCount; i += 2)
        {
            yield return new Fixture(i - 1, i) { Round = 1 };
        }
    }
}