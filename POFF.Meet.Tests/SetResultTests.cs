using NUnit.Framework;
using POFF.Meet.Domain;

namespace POFF.Meet.Tests;

[TestFixture]
public class SetResultTests
{
    private SetResult _sut;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _sut = new SetResult(home: 5, guest: 3);
    }

    [Test]
    public void Home_returns_home_goals()
    {
        Assert.That(_sut.Home, Is.EqualTo(5));
    }

    [Test]
    public void Guest_returns_guest_goals()
    {
        Assert.That(_sut.Guest, Is.EqualTo(3));
    }

    [Test]
    public void ToString_returns_home_colon_guest()
    {
        Assert.That(_sut.ToString(), Is.EqualTo("5:3"));
    }
}