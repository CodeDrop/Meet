using System;
using System.Collections.Generic;

namespace POFF.Meet.Domain.PlayModes.RoundRobin;

public class RoundRobinPlayMode : PlayMode
{
    private readonly List<Fixture> _matches = [];
    private int _teamsCount;

    public RoundRobinPlayMode() : base("Round Robin")
    { }

    public override IEnumerable<Fixture> Generate(int teamsCount, int round = 0)
    {
        _teamsCount = teamsCount;
        var startIndex = default(int);
        int matchCount = (int)Math.Round((_teamsCount - 1) * _teamsCount / 2d);
        _matches.Clear();

        // Build a section of one more than half the team count
        int sectionCount = (int)Math.Round(_teamsCount / 2d) + 1;

        while (_matches.Count < matchCount)
        {
            int matchNumber = _matches.Count + 1;
            FindNextSectionMatch(matchNumber, startIndex, sectionCount);               // Gruppe 1
            if (sectionCount > 1)
            {
                FindNextSectionMatch(matchNumber, startIndex + sectionCount - 1, sectionCount); // Gruppe 2
            }

            // Change group zusammensetzung
            startIndex += 1;
        }

        return _matches;
    }

    private void FindNextSectionMatch(int matchNumber, int startIndex, int groupCount)
    {
        int team1Index, team2Index; // "real" index calculated from "circular" index (identical while cIndex<length)

        for (int team1CIndex = startIndex, loopTo = startIndex + groupCount - 2; team1CIndex <= loopTo; team1CIndex++)
        {
            team1Index = GetTeamIndex(_teamsCount, team1CIndex);

            for (int team2CIndex = team1CIndex + 1, loopTo1 = startIndex + groupCount - 1; team2CIndex <= loopTo1; team2CIndex++)
            {
                team2Index = GetTeamIndex(_teamsCount, team2CIndex);

                if (!MatchExists(team1Index, team2Index))
                {
                    var matchIndexPair = new Fixture(team1Index, team2Index);

                    if (_matches.Count < matchNumber)
                    {
                        _matches.Add(matchIndexPair);
                    }
                    else
                    {
                        _matches[matchNumber - 1] = matchIndexPair;
                    }
                    return;
                }
            }
        }
    }

    private int GetTeamIndex(int teamsCount, int circularIndex)
    {
        int index = circularIndex;

        while (!(index < teamsCount))
            index -= teamsCount;

        return index;
    }

    private bool MatchExists(int team1Index, int team2Index)
    {
        foreach (var matchIndexPair in _matches)
        {
            if (team1Index == matchIndexPair.Item1 & team2Index == matchIndexPair.Item2 | team1Index == matchIndexPair.Item2 & team2Index == matchIndexPair.Item1)

            {
                return true; // Match found
            }
        }

        return false;
    }

}