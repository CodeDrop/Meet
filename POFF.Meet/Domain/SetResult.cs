namespace POFF.Meet.Domain;

public class SetResult(in int home, in int guest)
{
    public int Home { get; } = home;

    public int Guest { get; } = guest;

    public override string ToString()
    {
        return string.Format("{0}:{1}", Home, Guest);
    }
}