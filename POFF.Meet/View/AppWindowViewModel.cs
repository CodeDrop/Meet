using POFF.Meet.Domain;
using POFF.Meet.Domain.PlayModes;
using POFF.Meet.Domain.ScoreModes;
using POFF.Meet.Extensions;
using POFF.Meet.Infrastructure;
using POFF.Meet.View.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace POFF.Meet.View;

public class AppWindowViewModel : ViewModelBase
{
    private Tournament _tournament = Tournament.Empty;

    public AppWindowViewModel()
    {
        SetOptionLists();
        SetTeamsMatchesAndStandings();
    }

    private void SetOptionLists()
    {
        // Use reflection to discover all concrete types implementing IPlayMode in this assembly
        var playModeTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(PlayMode).IsAssignableFrom(t)
                && !t.IsAbstract
                && !t.IsInterface
                && t.GetConstructor(Type.EmptyTypes) != null
            );

        PlayModes.SetValues(playModeTypes.Select(t => Activator.CreateInstance(t) as PlayMode));
    }

    public void NewTournament()
    {
        _tournament = new Tournament { Name = "New Tournament" };
        SetTeamsMatchesAndStandings();
        SetTitleAndDirtyFlag(true);
        IsNew = true;
    }

    public void Open()
    {
        _tournament = Storage.Load();
        SetTeamsMatchesAndStandings();
        SetTitleAndDirtyFlag(false);
        OnPropertyChanged(nameof(PlayMode));
        IsNew = false;
    }

    public void Save()
    {
        Storage.Save(_tournament);
        SetTitleAndDirtyFlag(false);
    }

    public ITournamentStorage Storage { get; set; }

    private void SetTitleAndDirtyFlag(bool isDirty = false)
    {
        IsDirty = isDirty;
        Title = $"POFF Meet [{_tournament.Name}]" + (IsDirty ? "*" : "");
    }

    private void SetTeamsMatchesAndStandings()
    {
        Teams.SetValues(_tournament.Teams);
        Matches.SetValues(_tournament.Matches);
        Standings.SetValues(_tournament.GetStandings());
    }

    public bool IsNew { get; private set; }

    public BindingList<Team> Teams { get; } = [];

    public BindingList<PlayMode> PlayModes { get; } = [];

    public PlayMode PlayMode
    {
        get => _tournament.PlayMode;
        set
        {
            if (value != null && !value.Equals(_tournament.PlayMode))
            {
                _tournament.PlayMode = value;
                Matches.SetValues(_tournament.Matches);
                Standings.SetValues(_tournament.GetStandings());
                SetTitleAndDirtyFlag(true);
            }
            OnPropertyChanged(nameof(PlayMode));
        }
    }

    public BindingList<Match> Matches { get; } = [];

    public BindingList<Standing> Standings { get; } = [];

    public Team SelectedTeam { get; set; }

    public Match SelectedMatch { get; set; }

    public IEnumerable<Match> SelectedMatches { get; set; }

    public string MatchFilter
    {
        set
        {
            var matches = _tournament.Matches.Where(m => string.IsNullOrEmpty(value) ||
                m.Team1.Name == value ||
                m.Team2.Name == value);
            Matches.SetValues(matches);
        }
    }

    public bool IsDirty { get; private set; }

    public string Title
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public string TournamentId => _tournament.Id.ToString();

    public void AddTeam(TeamInfo teamInfo)
    {
        var newTeam = _tournament.AddTeam(teamInfo.Name);
        Teams.Add(newTeam);
        Matches.SetValues(_tournament.Matches);
        Standings.SetValues(_tournament.GetStandings());
        SetTitleAndDirtyFlag(true);
    }

    public void UpdateTeam(TeamInfo teamInfo)
    {
        if (SelectedTeam is null) return;
        SelectedTeam.Name = teamInfo.Name;
        Matches.SetValues(_tournament.Matches);
        Standings.SetValues(_tournament.GetStandings());
        SetTitleAndDirtyFlag(true);
    }

    public void RemoveTeam()
    {
        if (SelectedTeam is not null)
        {
            _tournament.RemoveTeam(SelectedTeam);
            Teams.Remove(SelectedTeam);
            Matches.SetValues(_tournament.Matches);
            Standings.SetValues(_tournament.GetStandings());
            SetTitleAndDirtyFlag(true);
        }
    }

    public void ProcessResult(BindingList<SetResultInput> setResults)
    {
        if (SelectedMatch is null) return;

        SelectedMatch.Result.Clear();

        foreach (SetResultInput input in setResults)
        {
            if (input.Home.HasValue && input.Guest.HasValue)
            {
                SelectedMatch.Result.AddSetResult(new SetResult(input.Home.Value, input.Guest.Value));
            }
        }
        SelectedMatch.Status = SelectedMatch.Result.SetResults.Any() ? MatchStatus.Finished : MatchStatus.Open;
        Matches.ResetBindings();
        Standings.SetValues(_tournament.GetStandings());
        SetTitleAndDirtyFlag(true);
    }

    public void CopyToClipboard(ExportType exportType)
    {
        var exporter = new ClipboardHtmlExporter(exportType);
        exporter.Export(_tournament);
    }

    public int TotalMatchCount()
    {
        return _tournament.Matches.Count(m => !ContainsWithdrawnTeam(m));
    }

    public int PlayedMatchCount()
    {
        return _tournament.Matches.Count(m => m.Status == MatchStatus.Finished);
    }

    private bool ContainsWithdrawnTeam(Match match)
    {
        if (!Teams.Any()) return false;
        var team1 = Teams.Single(t => t.Equals(match.Team1));
        var team2 = Teams.Single(t => t.Equals(match.Team2));
        return team1.Withdrawn | team2.Withdrawn;
    }

    internal void ExportInto(string fileName)
    {
        var exporter = new TwigFileInjectionExporter(fileName);
        if (SelectedMatches is null || !SelectedMatches.Any())
        {
            exporter.Export(_tournament);
        }
        else
        {
            var matchNumbers = SelectedMatches?.Select(m => m.Number) ?? [];
            exporter.Export(_tournament, matchNumbers);
        }
    }
}