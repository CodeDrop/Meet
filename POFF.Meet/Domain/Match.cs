using System.Diagnostics;

namespace POFF.Meet.Domain;

[DebuggerDisplay("Match N°{Number}: {Team1} - {Team2}")]
public class Match(in int number, in Team team1, in Team team2)
{
    public int Number { get; } = number;
    public Team Team1 { get; } = team1;
    public Team Team2 { get; } = team2;
    public int Section { get; set; } = 0;
    public MatchStatus Status { get; set; }
    public MatchResult Result { get; set; } = new MatchResult();

    public new bool Equals(object obj)
    {
        var otherMatch = (Match)obj;
        if (otherMatch.Team1.Equals(Team1) && otherMatch.Team2.Equals(Team2))
            return true;
        if (otherMatch.Team1.Equals(Team2) && otherMatch.Team2.Equals(Team1))
            return true;

        return false;
    }
}