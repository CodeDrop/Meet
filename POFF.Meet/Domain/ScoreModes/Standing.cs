using System;

namespace POFF.Meet.Domain.ScoreModes;

public class Standing(in Team team) : IComparable
{
    public int Place { get; set; }
    public Team Team { get; } = team;
    public int Score { get; set; }
    public WinDrawLoss Matches { get; set; }
    public ScoredConceded Frames { get; set; }
    public int MatchCount { get; set; }

    public int CompareTo(object obj)
    {
        Standing standing = (Standing)obj;
        return Place.CompareTo(standing.Place);
    }

    public bool Equals(Team obj)
    {
        return obj.Equals(Team);
    }
}