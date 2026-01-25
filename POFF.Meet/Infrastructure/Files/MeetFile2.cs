using POFF.Meet.Domain;
using POFF.Meet.View.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace POFF.Meet.Infrastructure.Files;

[XmlRoot("Meet")]
public class MeetFile2 : MeetFileBase
{
    [XmlElement("Id")]
    public Guid Id { get; set; }

    [XmlElement("PlayMode")]
    public string PlayMode { get; set; }

    [XmlArray("Teams")]
    [XmlArrayItem("Team")]
    public List<TeamEntry> Teams { get; set; }

    [XmlArray("Matches")]
    [XmlArrayItem("Match")]
    public List<MatchEntry> Matches { get; set; }

    [XmlArray("Results")]
    [XmlArrayItem("Result")]
    public List<ResultEntry> Results { get; set; }

    public override Tournament ToTournament()
    {
        var teams = Teams.Select(t => new Team { Number = t.No, Name = t.Name, Withdrawn = t.Withdrawn }).AsQueryable();

        var matches = Matches.Select(m => new Match()
        {
            Number = m.No,
            Section = m.Round,
            Team1 = teams.Single(t => t.Number == m.HomeNo),
            Team2 = teams.Single(t => t.Number == m.GuestNo),
            Result = GetResult(m.HomeNo, m.GuestNo),
            Status = m.Status
        });

        var playMode = GetPlayMode(PlayMode);

        return new Tournament(Id, teams, matches, playMode);
    }

    private MatchResult GetResult(int homeNo, int guestNo)
    {
        var resultEntry = Results.SingleOrDefault(r => r.HomeNo == homeNo && r.GuestNo == guestNo);
        var setResults = resultEntry == null ? [] :
            Results.Single(r => r.HomeNo == homeNo && r.GuestNo == guestNo)
                .Sets.Select(s => new SetResult(s.Home, s.Guest)).ToArray();

        return new MatchResult(setResults);
    }
}

public class TeamEntry
{
    [XmlAttribute("No")]
    public int No { get; set; }

    [XmlAttribute("Withdrawn")]
    public bool Withdrawn { get; set; }

    [XmlText]
    public string Name { get; set; }
}

public class MatchEntry
{
    [XmlAttribute("No")]
    public int No { get; set; }

    [XmlAttribute("Round")]
    public int Round { get; set; }

    [XmlAttribute("HomeNo")]
    public int HomeNo { get; set; }

    [XmlAttribute("GuestNo")]
    public int GuestNo { get; set; }

    [XmlText]
    public MatchStatus Status { get; set; }
}

public class ResultEntry
{
    [XmlAttribute("HomeNo")]
    public int HomeNo { get; set; }

    [XmlAttribute("GuestNo")]
    public int GuestNo { get; set; }

    [XmlArray("Sets")]
    [XmlArrayItem("Set")]
    public List<SetEntry> Sets { get; set; }
}

public class SetEntry
{
    [XmlAttribute("Home")]
    public int Home { get; set; }

    [XmlAttribute("Guest")]
    public int Guest { get; set; }
}
