using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.Models
{
    class Wordset
    {
        private const string PATH = @"D:/WORD_LIST.txt";

        private DateTime startTime;
        private Vocabulary dict;
        public List<Word> Set { get; private set; }
        public int Right { get; private set; }
        public int Wrong { get; private set; }
        public string Time { get; private set; }
        public string Percent { get; private set; }
        public bool IsLearning { get; set; } = false;
        public delegate DateTime TimeHandler(double time);
        public static double[] Coeff = new double[6] { 0.0, 1.0, 1.5, 2.333333333, 7.5, 73.0 };
        public Status Status { get; set; } = new Status();

        public Wordset(int amount, Vocabulary vocabulary, Category category, bool isLearning)
        {
            startTime = DateTime.Now;
            dict = vocabulary;
            IsLearning = isLearning;

            List<int> exceptions = Setting.Exceptions;

            TimeHandler Handler = DateTime.Now.AddDays;

            List<Word> set = GetInitialList(vocabulary, category);

            if (!IsLearning)
            {
                Set = set.OrderBy(x => Guid.NewGuid())
                    .Take(amount)
                    .ToList();
            }
            else
            {
                int wordAmount = amount;

                var set1 = set.Where(x => x.Progress >= 5 && Handler(-(x.Group * Coeff[x.Group])) > x.Time)
                    .OrderByDescending(x => x.Time)
                    .ThenBy(x => Guid.NewGuid())
                    .ToList();

                wordAmount -= set1.Count;
                List<Word> moreSet = GetInitialList(vocabulary, category);

                var set2 = moreSet.Distinct()
                    .Where(x => x.Dict.Name == vocabulary.Name)
                    .Where(x => x.Progress < 5)
                    .OrderByDescending(x => x.Time > new DateTime())
                    .OrderByDescending(x => x.Progress)
                    .ThenBy(x => Guid.NewGuid())
                    .Take(wordAmount)
                    .OrderBy(x => Guid.NewGuid())
                    .ToList();

                Set = set1.Union(set2)
                    .ToList();
            }

            SaveWordList();
        }

        public bool PrepareSet()
        {
            if (Set.Count > 0)
                return true;
            else
                return false;
        }

        public void Remove(string correct)
        {
            if (correct == "1")
            {
                Right++;

                if (IsLearning)
                {
                    Set[0].MakeProgress();
                }
            }
            else
            {
                Wrong++;

                if (IsLearning)
                {
                    Set[0].LowerProgress();
                }
            }

            if (Set.Count > 0)
                Set.RemoveAt(0);
        }

        // ONLY FOR TEST!
        public void SaveWordList()
        {
            string path = PATH;
            StringBuilder sb = new StringBuilder();

            foreach (var item in Set)
            {
                Example example = item.GetExample();
                int a = example.Indexes[0, 0];
                int b = example.Indexes[0, 1];

                string exampleStr = item.PartOfSpeech != PartOfSpeech.Sentence && a == 0 && b == 0 ? "-------------------> [NO EXAMPLE!]" : "";

                if (exampleStr != "")
                    sb.AppendLine($"{item.Name} - {item.Translation} {exampleStr}");
            }

            File.WriteAllText(path, sb.ToString());
        }

        public List<Word> GenerateVariants(Word word)
        {
            var result = Word.Vocabulary.Distinct()
                .OrderBy(x => Guid.NewGuid())
                .Where(x => x.Dict.Name == dict.Name)
                .Where(x => x != word && x.PartOfSpeech == word.PartOfSpeech)
                .Take(3)
                .ToList()
                .Union(
                new List<Word>() { word })
                .OrderBy(x => Guid.NewGuid())
                .ToList();

            return result;
        }

        public void ShowWordStatus()
        {
            if (Set.Count <= 0)
                return;

            if (!IsLearning)
                return;

            if (Set[0].Progress == -1 && Set[0].Group == 0)
            {
                Status.Text = "New!";
                Status.Color = Brushes.Firebrick;
            }
            else if (Set[0].Progress >= 5 && Set[0].Group >= 1)
            {
                Status.Text = "R";
                Status.Color = Brushes.ForestGreen;
            }
            else
                Status.Text = "";
        }

        public void CalculateResults()
        {
            Status.Text = "";

            TimeSpan time = DateTime.Now - startTime;
            Time = time.ToString(@"hh\:mm\:ss");

            double percent = (Convert.ToDouble(Right) / (Convert.ToDouble(Right) + Convert.ToDouble(Wrong))) * 10.0;
            Percent = Math.Floor(percent).ToString() + "/10";
        }

        public void SaveProgress()
        {
            Word.SaveWordsToFile(dict);
            Word.SaveWordsToFile(Vocabulary.Fav);
        }

        private List<Word> GetInitialList(Vocabulary vocabulary, Category category)
        {
            List<Word> set = new List<Word>();

            set = Word.Vocabulary.Distinct()
                .Where(x => x.Dict.Name == vocabulary.Name)
                .ToList();

            if (category.Name != "All")
            {
                set = set.Where(x => x.Category != null && x.Category.Name == category.Name)
                    .ToList();
            }

            var set1 = Setting.ShowWords ? set.Where(x => x.WordClass == WordClass.Word)
                .ToList() : new List<Word>();
            var set2 = Setting.ShowPhrases ? set.Where(x => x.WordClass == WordClass.Phrase)
                .ToList() : new List<Word>();
            var set3 = Setting.ShowSentences ? set.Where(x => x.WordClass == WordClass.Sentence)
                .ToList() : new List<Word>();

            set = set1.Union(set2)
                .Union(set3)
                .ToList();

            return set;
        }
    }
}
