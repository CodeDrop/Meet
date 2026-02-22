using NUnit.Framework;
using POFF.Meet.Domain;
using POFF.Meet.View.Model;
using System.Linq;

namespace POFF.Meet.Tests.PlayModes;

[TestFixture]
public class SwissSystemPlayModeTests
{
    private SwissSystemPlayMode _sut;
    private Tournament _tournament;

    [SetUp]
    public void SetUp()
    {
        _tournament = new Tournament();
        _sut = new SwissSystemPlayMode(_tournament);
        _tournament.PlayMode = _sut;
    }

    [Test]
    public void Swiss_System_generates_round_1()
    {
        Assert.That(_sut.Generate(4).All(f => f.Round == 1), Is.True);
    }

    [Test]
    public void Round0_has_zero_fixtures()
    {
        var round0 = _sut.Generate(teamsCount: 10, round: 0);
        Assert.That(round0.Count(), Is.Zero);
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(3, 0)]
    [TestCase(4, 2)]
    public void Round1_has_expected_number_of_fixtures(int teamsCount, int expectedFixtureCount)
    {
        var round1 = _sut.Generate(teamsCount, 1);
        Assert.That(round1.Count(), Is.EqualTo(expectedFixtureCount));
    }

    [Test]
    public void Round2_has_zero_fixtures_when_no_games_played()
    {
        var round2 = _sut.Generate(teamsCount: 4, round: 2);
        Assert.That(round2.Count(), Is.Zero);
    }

    [Test]
    public void Round2_has_same_number_of_fixtures()
    {
        // arrange
        new string[] { "T1", "T2", "T3", "T4" }.ToList().ForEach(t => _tournament.AddTeam(t));
        _tournament.SetResult(1, new MatchResult([new SetResult(1, 0)]));
        _tournament.SetResult(2, new MatchResult([new SetResult(1, 0)]));

        // act
        var round2 = _sut.Generate(teamsCount: 4, round: 2);

        // assert
        Assert.That(round2.Count(), Is.EqualTo(2));
    }
}
