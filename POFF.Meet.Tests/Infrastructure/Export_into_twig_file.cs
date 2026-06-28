using NUnit.Framework;
using POFF.Meet.Domain;
using POFF.Meet.Infrastructure;
using POFF.Meet.Infrastructure.Files;
using POFF.Meet.View.Model;
using System.IO;

namespace POFF.Meet.Tests.Infrastructure;

[TestFixture]
public class Export_into_twig_file
{
    private Tournament _tournament;
    private TwigFileInjectionExporter _sut;
    private string _targetFile;
    private string _content;

    [OneTimeSetUp]
    public void SetUp()
    {
        _targetFile = Path.GetTempFileName();
        File.Copy(@"Infrastructure\TournamentPage.html.twig", _targetFile, overwrite: true);
        var storage = new FileTournamentStorage(@"Infrastructure\MeetFile1.xml");
        _tournament = storage.Load();
        _sut = new TwigFileInjectionExporter(_targetFile);
        _sut.Export(_tournament, [1]);
        _content = File.ReadAllText(_targetFile);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        if (File.Exists(_targetFile))
        {
            File.Delete(_targetFile);
        }
    }

    [Test]
    [TestCase("<!-- Meet#2545D2E5-380F-4F42-A46A-2C4ABDCB54AF#Ranking-Start -->")]
    [TestCase("<!-- Meet#2545D2E5-380F-4F42-A46A-2C4ABDCB54AF#Ranking-End -->")]
    public void File_contains_ranking_tag(string tag)
    {
        Assert.That(_content, Does.Contain(tag));
    }

    [Test]
    [TestCase("<tr><td>1</td><td>Team B</td>")]
    [TestCase("<tr><td>2</td><td>Team A</td>")]
    public void File_contains_ranking_HTML(string ranking)
    {
        Assert.That(_content, Does.Contain(ranking));
    }

    [Test]
    [TestCase("<!-- Meet#2545D2E5-380F-4F42-A46A-2C4ABDCB54AF#Games-Start -->")]
    [TestCase("<!-- Meet#2545D2E5-380F-4F42-A46A-2C4ABDCB54AF#Games-End -->")]
    public void File_contains_games_tag(string tag)
    {
        Assert.That(_content, Does.Contain(tag));
    }

    [Test]
    [TestCase("<td>1</td><td>Team A</td><td>(3:0)</td><td>Team B</td>")]
    public void File_contains_game_HTML(string ranking)
    {
        Assert.That(_content, Does.Contain(ranking));
    }

    [Test]
    [TestCase("<td>2</td><td>Team B</td><td>(2:1)</td><td>Team C</td>")]
    public void File_does_not_contain_game_HTML(string ranking)
    {
        Assert.That(_content, Does.Not.Contain(ranking));
    }

    [Test]
    public void File_contains_all_team_names()
    {
        foreach (Team team in _tournament.Teams)
        {
            Assert.That(_content, Does.Contain(team.Name));
        }
    }
}
