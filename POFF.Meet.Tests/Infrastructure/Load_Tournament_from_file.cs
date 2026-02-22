using NUnit.Framework;
using POFF.Meet.Domain;
using POFF.Meet.Domain.PlayModes.RoundRobin;
using POFF.Meet.Infrastructure.Files;
using POFF.Meet.View.Model;
using System;
using System.Linq;

namespace POFF.Meet.Tests.Infrastructure;

[TestFixture]
public class Load_Tournament_from_file
{
    private Tournament _sut;

    [OneTimeSetUp]
    public void Setup()
    {
        var storage = new FileTournamentStorage(@"Infrastructure\MeetFile2.xml");
        _sut = storage.Load();
    }

    [Test]
    public void File_contains_id()
    {
        Assert.That(_sut.Id, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public void File_contains_playmode()
    {
        Assert.That(_sut.PlayMode, Is.InstanceOf<RoundRobinPlayMode>());
    }

    [Test]
    public void File_contains_teams()
    {
        Assert.That(_sut.Teams, Is.Not.Empty);
    }

    [Test]
    public void File_contains_matches()
    {
        Assert.That(_sut.Matches, Is.Not.Empty);
    }

    [Test]
    public void File_contains_match_status()
    {
        Assert.That(_sut.Matches.ElementAt(0).Status, Is.EqualTo(MatchStatus.Finished));
    }

    [Test]
    public void File_contains_match_results()
    {
        Assert.That(_sut.Matches.ElementAt(0).Result.SetResults, Is.Not.Empty);
    }
}
