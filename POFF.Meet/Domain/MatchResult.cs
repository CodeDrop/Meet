using System;
using System.Collections.Generic;
using System.Linq;

namespace POFF.Meet.Domain;

public class MatchResult
{
    public MatchResult()
    { }

    public MatchResult(IEnumerable<SetResult> setResults)
    {
        _setResults.AddRange(setResults);
    }

    private readonly List<SetResult> _setResults = [];
    public IEnumerable<SetResult> SetResults => _setResults;

    public void AddSetResult(SetResult setResult)
    {
        if (setResult is null)
            throw new ArgumentNullException("setResultRow");
        _setResults.Add(setResult);
    }

    public void Clear()
    {
        _setResults.Clear();
    }

    public int Home()
    {
        if (_setResults.Count == 1)
        {
            return _setResults[0].Home;
        }
        return SetResults.Count(sr => sr.Home > sr.Guest);
    }

    public int Guest()
    {
        if (_setResults.Count == 1)
        {
            return _setResults[0].Guest;
        }
        return SetResults.Count(sr => sr.Guest > sr.Home);
    }

    public override string ToString()
    {
        if (!SetResults.Any()) return "";

        var resultString = new System.Text.StringBuilder();

        foreach (var setResult in SetResults)
            resultString.AppendFormat("/{0}", setResult);

        return resultString.ToString().Substring(1);
    }
}