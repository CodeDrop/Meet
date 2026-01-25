using POFF.Meet.View.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace POFF.Meet.Infrastructure.Files;

public class FileTournamentStorage(in string filename) : ITournamentStorage
{
    private readonly string _filename = filename;

    public void Save(Tournament tournament)
    {
        var file = new MeetFile2
        {
            Id = tournament.Id,
            Teams = [.. tournament.Teams.Select(t => new TeamEntry { No = t.Number, Name = t.Name, Withdrawn = t.Withdrawn })],
            Matches = [.. tournament.Matches.Select(m => new MatchEntry { No = m.Number, HomeNo = m.Team1.Number, GuestNo = m.Team2.Number, Round = m.Section, Status = m.Status })],
            Results = [.. GetPlayedResults(tournament.Matches)],
            PlayMode = tournament.PlayMode.GetType().Name,
        };
        var writer = new StreamWriter(_filename, false);
        var serializer = new XmlSerializer(file.GetType());
        serializer.Serialize(writer, file);
        writer.Close();
        tournament.Name = Path.GetFileNameWithoutExtension(_filename);
    }

    private IEnumerable<ResultEntry> GetPlayedResults(IEnumerable<Domain.Match> matches)
    {
        foreach (var match in matches.Where(m => m.Status != Domain.MatchStatus.Open))
        {
            var resultEntry = new ResultEntry
            {
                HomeNo = match.Team1.Number,
                GuestNo = match.Team2.Number,
                Sets = match.Result.SetResults.Select(s => new SetEntry { Home = s.Home, Guest = s.Guest }).ToList()
            };
            yield return resultEntry;
        }
    }

    public Tournament Load()
    {
        if (!File.Exists(_filename)) return Tournament.Empty;

        var meetFile = Deserialize([typeof(MeetFile2)]);
        var tournament = meetFile.ToTournament();

        if (string.IsNullOrWhiteSpace(tournament.Name))
        {
            tournament.Name = Path.GetFileNameWithoutExtension(_filename);
        }

        return tournament;
    }

    private MeetFileBase Deserialize(IEnumerable<Type> meetFileTypes)
    {
        using var stream = File.OpenRead(_filename);
        foreach (var type in meetFileTypes)
        {
            var serializer = new XmlSerializer(type);
            try
            {
                return (MeetFileBase)serializer.Deserialize(stream);
            }
            catch (InvalidOperationException)
            {
                // Ignore and try next type
            }
            finally
            {
                stream.Seek(0, SeekOrigin.Begin); // Reset stream position for next attempt
            }
        }
        return null;
    }
}