using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordRatingAPI;
using WebsterThesaurusAPI;
using System.Text.RegularExpressions;

namespace SpewspeakActualReal.Model
{
    static class Conversion
    {
        #region Members definition
        public const string API_KEY = "69b8b0b3-2427-482c-86b2-3d6d83214f92";

        public static List<string> ReservedWords = new List<string>()
        {
            "the",
            "it",
            "who",
            "we",
            "you",
            "i",
        };

        private const string WORD_DELIMITER_SIMPLE = " ";
        #endregion

        #region Methods definition
        public static string ConvertSentence(string sentence)
        {
            StringBuilder convertedSentenceBuilder = new StringBuilder();

            string[] splitSentence = Regex.Split(sentence, WORD_DELIMITER_SIMPLE);

            foreach (string word in splitSentence)
            {
                if (ReservedWords.Contains<string>(word))
                {
                    convertedSentenceBuilder.Append(word);
                }
                else
                {
                    convertedSentenceBuilder.Append(MostComplexSynonym(word));
                }
                convertedSentenceBuilder.Append(WORD_DELIMITER_SIMPLE);
            }

            return convertedSentenceBuilder.ToString();
        }

        public static string MostComplexSynonym(string word)
        {
            WebsterConnection webConn = new WebsterConnection(API_KEY);
            string mostComplexWord = "";

            List<string> synonymsAndWord = webConn.GetSynonyms(word);
            if (!synonymsAndWord.Contains(word))
            {
                synonymsAndWord.Add(word);
            }

            foreach (string cw in synonymsAndWord)
            {
                if (mostComplexWord == "" || WordRater.CompareWords(mostComplexWord, cw) == cw) { mostComplexWord = cw; }
            }

            return mostComplexWord;
        }
        #endregion
    }
}
