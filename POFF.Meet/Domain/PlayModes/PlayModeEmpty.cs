using System.Collections.Generic;

namespace POFF.Meet.Domain.PlayModes;

public class PlayModeEmpty : PlayMode
{
    public PlayModeEmpty() : base(string.Empty)
    { }

    public override IEnumerable<Fixture> Generate(int teamsCount, int round = 0) => [];
}