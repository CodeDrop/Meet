using System;
using System.Linq;

namespace POFF.Meet.Domain;

public class MatchResult
{
    public SetResult[] SetResults = [];

    public void AddSetResult(SetResult setResult)
    {
        if (setResult is null)
            throw new ArgumentNullException("setResultRow");

        int goalCount1 = default, goalCount2 = default;

        goalCount1 = setResult.Home;
        goalCount2 = setResult.Guest;

        Array.Resize(ref SetResults, SetResults.Length + 1);
        SetResults[SetResults.Length - 1] = new SetResult();
        SetResults[SetResults.Length - 1].Home = goalCount1;
        SetResults[SetResults.Length - 1].Guest = goalCount2;
    }

    public void Clear()
    {
        Array.Resize(ref SetResults, 0);
    }

    public int Home()
    {
        if (SetResults.Length == 1)
        {
            return SetResults[0].Home;
        }
        return SetResults.Count(sr => sr.Home > sr.Guest);
    }

    public int Guest()
    {
        if (SetResults.Length == 1)
        {
            return SetResults[0].Guest;
        }
        return SetResults.Count(sr => sr.Guest > sr.Home);
    }

    public override string ToString()
    {
        if (SetResults.Length == 0)
            return "";

        var resultString = new System.Text.StringBuilder();

        foreach (var setResult in SetResults)
            resultString.AppendFormat("/{0}", setResult);

        return resultString.ToString().Substring(1);
    }
}