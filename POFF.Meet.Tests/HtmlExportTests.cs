using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using POFF.Meet.Domain;
using POFF.Meet.Infrastructure;
using POFF.Meet.View.Model;

namespace POFF.Meet.Tests;

[TestFixture, RequiresThread(ApartmentState.STA)]
public class HtmlExportTests
{
    private static string _result;

    [OneTimeSetUp]
    public static void SetUp()
    {
        // Arrange
        var tournament = new Tournament();
        tournament.AddTeam("Team N°1");
        tournament.AddTeam("Team N°2");
        tournament.AddTeam("Team N°3").Withdrawn = true;
        var matchResult = new MatchResult();
        matchResult.AddSetResult(new SetResult(home: 5, guest: 3));
        tournament.SetResult(2, matchResult);
        var sut = new ClipboardHtmlExporter(ExportType.Games | ExportType.Standings);

        // Act
        sut.Export(tournament);
        _result = sut.ToString();
    }

    [Test]
    public void StandingTest()
    {
        Assert.That(_result, Does.Contain("<td>Team N°1</td>"));
    }

    [Test]
    public void GamesOfWithdrawnTeam()
    {
        var pattern = new Regex("<td>Team N°3</td>");
        Assert.That(_result, Does.Not.Match(pattern.ToString()));
    }
}