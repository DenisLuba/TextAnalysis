namespace TextAnalysis;

using System.Collections.Generic;
using System.Linq;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();
        var pairs = new Dictionary<string, Dictionary<string, int>>();
        var triples = new Dictionary<string, Dictionary<string, int>>();

        foreach (var sentence in text)
        {
            SetPairsFromSentence(sentence, pairs);
            SetTriplesFromSentence(sentence, triples);
        }

        SetPairsToDictionary(pairs, result);
        SetPairsToDictionary(triples, result);

        return result;
    }
    
    private static void SetPairsFromSentence(
        List<string> sentence, Dictionary<string,
            Dictionary<string, int>> pairs
    )
    {
        for (var i = 0; i < sentence.Count - 1; i++)
        {
            if (pairs.ContainsKey(sentence[i]))
            {
                if (pairs[sentence[i]].ContainsKey(sentence[i + 1]))
                    pairs[sentence[i]][sentence[i + 1]]++;
                else pairs[sentence[i]].Add(sentence[i + 1], 1);
            }
            else pairs.Add(sentence[i], new Dictionary<string, int> { [sentence[i + 1]] = 1 });
        }
    }

    private static void SetTriplesFromSentence(
        List<string> sentence,
        Dictionary<string, Dictionary<string, int>> triples
    )
    {
        for (var i = 0; i < sentence.Count - 2; i++)
        {
            if (triples.ContainsKey($"{sentence[i]} {sentence[i + 1]}"))
            {
                if (triples[$"{sentence[i]} {sentence[i + 1]}"].ContainsKey(sentence[i + 2]))
                    triples[$"{sentence[i]} {sentence[i + 1]}"][sentence[i + 2]]++;
                else triples[$"{sentence[i]} {sentence[i + 1]}"].Add(sentence[i + 2], 1);
            }
            else triples.Add($"{sentence[i]} {sentence[i + 1]}", new Dictionary<string, int> { [sentence[i + 2]] = 1 });
        }
    }

    private static void SetPairsToDictionary(
        Dictionary<string, Dictionary<string, int>> pairs,
        Dictionary<string, string> result
    )
    {
        foreach (var (word1, value) in pairs)
        {
            var maxValue = value.Max(entry => entry.Value);
            var word2 = value
                .Where(entry => entry.Value == maxValue)
                .MinBy(entry => entry.Key)
                .Key;

            result.Add(word1, word2);
        }
    }
}