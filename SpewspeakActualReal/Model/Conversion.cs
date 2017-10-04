using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordRatingAPI;
using WebsterThesaurusAPI;
using System.Text.RegularExpressions;
using NHunspell;

namespace SpewspeakActualReal.Model
{
    static class Conversion
    {
        #region Members definition
        public const string API_KEY = "";

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
            WordNetClasses.WN wordNetIns = new WordNetClasses.WN(@"inc\WordNet\");
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
                    convertedSentenceBuilder.Append(MostComplexSynonymOffline(word));
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

        public static string MostComplexSynonymOffline(string word)
        {
            MyThes thesaurus = new MyThes(Properties.Resources.th_en_US_new);
            ThesResult lookupResult = thesaurus.Lookup(word);

            if (lookupResult == null)
            {
                // There are no synonyms for the word given.
                // Return the standard word.
                // TODO: THIS IS A CURRENT WORKAROUND METHOD. FUTURE VERSIONS SHOULD TRY TO CHECK IF THIS WORD IS A SPECIFIC TENSE OF A
                // VERB/ETC AND TRY TO PUT IT INTO A DIFFERENT TENSE AND LOOK THAT UP INSTEAD.
                return word;
            }

            Dictionary<string, List<ThesMeaning>> synonymsDict = lookupResult.GetSynonyms();
            string mostComplexWord = "";

            #region Synonym collection setup
            List<string> synonyms = new List<string>();
            foreach (string cword in synonymsDict.Keys)
            {
                // Get each key from the results Dictionary and add it to the list of synonyms.
                synonyms.Add(cword);
            }

            if (synonyms.Contains(word)) { /* Word is already in the list of synonyms. Do not do anything. */ }
            else { /* Word is not in the list of synonyms. Add it. */ synonyms.Add(word); }
            #endregion

            #region Most complex synonym choice
            foreach (string cw in synonyms)
            {
                // Check if the current word is more complex than the previous most complex word.
                // If so, replace the most complex word with the current word.
                if (mostComplexWord == "" || WordRater.CompareWords(mostComplexWord, cw) == cw) { mostComplexWord = cw; }
            }
            #endregion

            return mostComplexWord;
        }
        #endregion
    }
}
