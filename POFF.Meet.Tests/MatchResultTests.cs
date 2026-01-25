using NUnit.Framework;
using POFF.Meet.Domain;
using System.Linq;

namespace POFF.Meet.Tests;

[TestFixture]
public class MatchResultTests
{
    [Test]
    public void AddSetResultTest()
    {
        // Arrange
        var result = new MatchResult();
        var setResult = new SetResult(home: 5, guest: 2);

        // Act
        result.AddSetResult(setResult);

        // Assert
        Assert.That(result.SetResults, Is.Not.Null, "SetResults is null");
        Assert.That(result.SetResults.Count(), Is.EqualTo(1), "Unexpected number of SetResults");
        Assert.That(result.SetResults.First().Home, Is.EqualTo(5), "Unexpected SetResults[0].Home");
        Assert.That(result.SetResults.First().Guest, Is.EqualTo(2), "Unexpected SetResults[0].Guest");
    }

    [Test]
    public void HomeGuest_return_set_goals_counts()
    {
        // arrange
        var sut = new MatchResult();
        sut.AddSetResult(new SetResult(home: 73, guest: 16));

        // act & assert
        Assert.That(sut.Home(), Is.EqualTo(73));
        Assert.That(sut.Guest(), Is.EqualTo(16));
    }

    [Test]
    public void HomeGuest_return_set_counts()
    {
        // arrange
        var sut = new MatchResult();
        sut.AddSetResult(new SetResult(home: 73, guest: 16));
        sut.AddSetResult(new SetResult(home: 54, guest: 45));

        // act & assert
        Assert.That(sut.Home(), Is.EqualTo(2));
        Assert.That(sut.Guest(), Is.EqualTo(0));
    }
}