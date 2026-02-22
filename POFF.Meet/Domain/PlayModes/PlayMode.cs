using System.Collections.Generic;

namespace POFF.Meet.Domain.PlayModes;

public abstract class PlayMode(in string name)
{
    public static readonly PlayMode Empty = new PlayModeEmpty();

    public string Name { get; } = name;

    public abstract IEnumerable<Fixture> Generate(int teamsCount, int round = 0);

    public virtual bool IsIterative => false;

    public override bool Equals(object obj)
    {
        return obj is PlayMode otherPlayMode && otherPlayMode.GetType().Name == GetType().Name;
    }

    public override int GetHashCode()
    {
        return GetType().Name.GetHashCode();
    }
}
