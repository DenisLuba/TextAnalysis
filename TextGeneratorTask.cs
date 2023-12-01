namespace TextAnalysis;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        var phraseWords = GetWords(phraseBeginning);
            
        for (var i = 0; i < wordsCount; i++)
        {
            if (phraseWords.Count >= 2 
                && nextWords.ContainsKey($"{phraseWords[^2]} {phraseWords[^1]}"))
            {
                var nextWord = nextWords[$"{phraseWords[^2]} {phraseWords[^1]}"];
                phraseBeginning += $" {nextWord}";
                phraseWords.Add(nextWord);
            }
            else if (nextWords.ContainsKey(phraseWords.Last()))
            {
                var nextWord = nextWords[phraseWords[^1]];
                phraseBeginning += $" {nextWord}";
                phraseWords.Add(nextWord);
            }
            else return phraseBeginning;
        }
            
        return phraseBeginning;
    }
    
    private static List<string> GetWords(string phraseBeginning)
    {
        var phraseWords = new List<string>();
        var regex = new Regex(@"[a-z|'|A-Z]+");
        MatchCollection matches = regex.Matches(phraseBeginning);
        
        foreach (Match word in matches)
            phraseWords.Add(word.Value.ToLower());
            
        return phraseWords;
    }
}