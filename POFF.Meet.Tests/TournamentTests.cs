using System.Linq;
using NUnit.Framework;
using POFF.Meet.Domain;
using POFF.Meet.Domain.PlayModes.RoundRobin;
using POFF.Meet.View.Model;

namespace POFF.Meet.Tests;

[TestFixture]
public class TournamentTests
{
    [Test]
    public void TournamentTest()
    {
        // Arrange
        // -

        // Act
        var result = new Tournament();

        // Assert
        Assert.That(result.Teams, Is.Not.Null);
        Assert.That(result.Matches, Is.Not.Null);
    }

    [Test]
    public void GetStandingsTest()
    {
        // Arrange
        var testClass = new Tournament() { PlayMode = new RoundRobinPlayMode() };
        testClass.AddTeam("Team N°1");
        testClass.AddTeam("Team N°2");
        var matchResult = new MatchResult();
        matchResult.AddSetResult(new SetResult(home: 5, guest: 3));
        testClass.SetResult(1, matchResult);

        // Act
        var result = testClass.GetStandings();

        // Assert
        Assert.That(result, Is.Not.Null, "GetStandings returned null");
        Assert.That(result.Count(), Is.EqualTo(2), "Unexpected Count");
    }
}