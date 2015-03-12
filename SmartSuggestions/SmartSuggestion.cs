using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartSuggestionDemo
{

    public class SmartSuggestion
    {
        /// <summary>
        /// Finds the similar words for suggestion.
        /// </summary>
        /// <param name="listOfStringsToCheck">The list of strings to check.</param>
        /// <param name="source">The source word to check.</param>
        /// <returns>Similar word that exist from the list</returns>
        /// <returns>Similar suggestion word</returns>
        public static string FindSimilarWords(IEnumerable<string> listOfStringsToCheck, string source)
        {
            SmartSuggestion ss = new SmartSuggestion();

            Dictionary<double, string> ranks = new Dictionary<double, string>();
            foreach (var item in listOfStringsToCheck)
            {
                var rank = ss.CompareStringProcess(source, item);
                if (!ranks.Keys.Contains(rank))
                    ranks.Add(rank, item);
            }

            List<KeyValuePair<double, string>> sortedRanks = ranks.ToList();
            var result = sortedRanks.OrderByDescending(order => order.Key).ToList();

            if (result[0].Key > 0d)
                return result[0].Value;
            else
                return String.Empty;
        }

        /// <summary>
        /// Compares the strings process.
        /// </summary>
        /// <param name="str1">The STR1.</param>
        /// <param name="str2">The STR2.</param>
        /// <returns>Rank number</returns>
        public double CompareStringProcess(string str1, string str2)
        {
            List<string> pairs1 = WordPairs(str1.ToUpper());
            List<string> pairs2 = WordPairs(str2.ToUpper());

            int intersection = 0;
            int union = pairs1.Count + pairs2.Count;

            for (int i = 0; i < pairs1.Count; i++)
            {
                string pair1 = pairs1[i];
                for (int j = 0; j < pairs2.Count; j++)
                {
                    string pair2 = pairs2[j];
                    if (pair1.Equals(pair2))
                    {
                        intersection++;
                        pairs2.Remove(pair2);
                        break;
                    }
                }
            }
            return (2.0 * intersection) / union;
        }

        private List<string> WordPairs(string str)
        {
            List<string> allPairs = new List<string>();

            // Tokenize the string and put the tokens/words into an array
            string[] splitter = { "\\s" };
            string[] words = str.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

            // For each word
            for (int w = 0; w < words.Length; w++)
            {
                // Find the pairs of characters
                string[] pairsInWord = LetterPairs(words[w]);
                for (int p = 0; p < pairsInWord.Length; p++)
                {
                    allPairs.Add(pairsInWord[p]);
                }
            }
            return allPairs;
        }

        private string[] LetterPairs(string str)
        {
            int numPairs = str.Length - 1;
            string[] pairs = new string[numPairs];
            for (int i = 0; i < numPairs; i++)
            {
                pairs[i] = str.Substring(i, 2);
            }
            return pairs;
        }
    }
}
