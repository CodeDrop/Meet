using POFF.Meet.Domain;
using POFF.Meet.Domain.PlayModes;
using POFF.Meet.Domain.ScoreModes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POFF.Meet.View.Model;

public class Tournament
{
    private readonly List<Team> _teams = [];
    private readonly List<Match> _matches = [];
    private IScoreMode _scoreMode = new Win3Equal1Loss0ScoreMode();
    private PlayMode _playMode;
    public static readonly Tournament Empty = new(Guid.Empty, [], [], PlayMode.Empty);

    public Tournament() : this(Guid.NewGuid(), [], [], PlayMode.Empty)
    { }

    public Tournament(Guid id, IEnumerable<Team> teams, IEnumerable<Match> matches, PlayMode playMode)
    {
        Id = id;
        _teams.AddRange(teams);
        _matches.AddRange(matches);
        _playMode = playMode;
    }

    public Guid Id { get; }

    public string Name { get; set; }

    public IEnumerable<Team> Teams => _teams;

    public IEnumerable<Match> Matches => _matches;

    public PlayMode PlayMode
    {
        get => _playMode;
        set
        {
            _playMode = value ?? throw new ArgumentNullException(nameof(PlayMode));
            GenerateMatches();
        }
    }

    public IScoreMode ScoreMode
    {
        set { _scoreMode = value ?? throw new ArgumentNullException(nameof(ScoreMode)); }
    }

    public Team AddTeam(string teamName)
    {
        var number = _teams.Any() ? _teams.Max(t => t.Number) + 1 : 1;
        var newTeam = new Team
        {
            Number = number,
            Name = teamName ?? throw new ArgumentNullException(nameof(teamName))
        };
        _teams.Add(newTeam);
        GenerateMatches();
        return newTeam;
    }

    public void RemoveTeam(Team team)
    {
        _teams.Remove(team ?? throw new ArgumentNullException(nameof(team)));
        GenerateMatches();
    }

    private void GenerateMatches(int round = 1)
    {
        _matches.Clear();

        foreach (var fixture in PlayMode.Generate(_teams.Count, round))
        {
            var match = new Match(_matches.Count + 1, _teams[fixture.Item1], _teams[fixture.Item2]);
            match.Section = fixture.Round;
            _matches.Add(match);
        }
    }

    public void SetResult(int matchNo, MatchResult result)
    {
        if (matchNo < 1 | matchNo > _matches.Count)
            throw new IndexOutOfRangeException("matchNo may only have values between 1 and number of matches");

        _matches[matchNo - 1].Result = result;
        _matches[matchNo - 1].Status = MatchStatus.Finished;
    }

    public IEnumerable<Standing> GetStandings()
    {
        if (Matches.Any())
        {
            return _scoreMode.Evaluate([.. Matches]);
        }
        else if (Teams.Any())
        {
            return [.. Teams.Select(team => new Standing(team))];
        }
        return [];
    }
}