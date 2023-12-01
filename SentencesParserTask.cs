using System.Text.RegularExpressions;

namespace TextAnalysis;

static class SentencesParserTask
{
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        var regex = new Regex(@"[a-z|'|A-Z]+");
        
        foreach (var sentence in text.Split('.', '!', '?', ';', ':', '(', ')'))
        {           
            MatchCollection matches = regex.Matches(sentence);
            if (matches.Count <= 0) continue;
            var list = new List<string>();
            foreach (Match word in matches)
                list.Add(word.Value.ToLower());
            
            sentencesList.Add(list);
        }
        return sentencesList;
    }
}