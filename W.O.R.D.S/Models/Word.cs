using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace W.O.R.D.S.Models
{
    class Word
    {
        private const string PATH = @"D:/MERGED_VOCAB.txt";
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Translation { get; set; }
        public PartOfSpeech PartOfSpeech { get; set; }
        public Level Level { get; set; }
        public string Transcription { get; set; }
        public string Meaning { get; set; }
        public Category Category { get; set; }
        public Example Example { get; set; }
        public int Group { get; set; }
        public int Progress { get; set; }
        public DateTime Time { get; set; }
        public bool Favorite { get; set; }
        public static List<Word> Vocabulary { get; private set; } = new List<Word>();
        public static int Count { get; private set; } = 0;
        public static double[] Coeff = new double[6] { 0.0, 1.0, 1.5, 2.333333333, 7.5, 73.0 };
        public Vocabulary Dict { get; set; }

        public Word(string name, string translation, PartOfSpeech partOfSpeech, Level level, string transcription, string meaning, Category category, Example example, int stage, int progress, DateTime time, bool favorite)
        {
            Id = Count++;
            Name = name;
            Translation = translation;
            PartOfSpeech = partOfSpeech;
            Level = level;
            Transcription = transcription == "" ? "" : transcription; // [ + ]
            Meaning = meaning;
            Category = category;
            Example = example;
            Group = stage;
            Progress = progress;
            Time = time;
            Favorite = favorite;
            Vocabulary.Add(this);
        }

        public bool ShouldSerializeId()
        {
            return false;
        }

        public bool ShouldSerializeDict()
        {
            return false;
        }

        public bool ShouldSerializeTranscription()
        {
            Transcription = Transcription.Trim(new char[] { '[', ']' });

            return true;
        }

        public static List<Word> GetWordsFromVocabulary(Vocabulary vocabulary)
        {
            var result = Vocabulary.Where(x => x != null && x.Dict.Name == vocabulary.Name)
                .ToList();

            return result;
        }

        public static int CountWordsInVocabulary(Vocabulary vocabulary)
        {
            var result = Vocabulary.Where(x => x.Dict.Name == vocabulary.Name)
                .ToList();

            return result.Count;
        }

        public static int CountLearningWords(Vocabulary vocabulary)
        {
            var result = Vocabulary.Where(x => x.Dict.Name == vocabulary.Name && x.Progress >= 0 && x.Group < 1)
                .ToList();

            return result.Count;
        }

        public static int CountRepeatWords(Vocabulary vocabulary)
        {
            var result = Vocabulary.Where(x => x.Dict.Name == vocabulary.Name && x.Progress >= 5 && DateTime.Now.AddDays(-(x.Group * Coeff[x.Group])) > x.Time)
                .ToList();

            return result.Count;
        }

        public static int CountStudiedWords(Vocabulary vocabulary)
        {
            var result = Vocabulary.Where(x => x.Dict.Name == vocabulary.Name && x.Progress >= 0 && DateTime.Now.AddDays(-(x.Group * Coeff[x.Group])) <= x.Time)
                .ToList();

            return result.Count;
        }

        public static void SaveWordsToFile(Vocabulary vocabulary)
        {
            string path = @"Files/Dictionaries/" + vocabulary.Path;

            StringBuilder sb = new StringBuilder();

            foreach (var word in GetWordsFromVocabulary(vocabulary))
            {
                sb.AppendLine(JsonConvert.SerializeObject(word));
            }

            File.WriteAllText(path, sb.ToString());
        }

        public static void DeleteWordFromFavs(string name, string translation)
        {
            Word delWord;

            foreach (var word in Vocabulary)
            {
                if (word.Dict.Name == "Favorites" && word.Name == name && word.Translation == translation)
                {
                    delWord = word;
                    Vocabulary.Remove(delWord);
                    delWord = null;

                    return;
                }
            }
        }

        public static bool IsAlreadyInVocabulary(string wordName, string dictName)
        {
            foreach (var word in Vocabulary)
            {
                if (word.Dict.Name == dictName && word.Name == wordName)
                {
                    return true;
                }
            }

            return false;
        }

        // ONLY FOR TEST!
        public static void FindRepeats()
        {
            string path = PATH;
            StringBuilder sb = new StringBuilder();

            var result = Vocabulary.Distinct().Where(x => x.Dict.Name != "All Essential Words").OrderBy(x => x.Name).ToList();

            foreach (var item in result)
            {
                sb.AppendLine($"{item.Name} - {item.Translation} ({item.Dict.Name})");
            }

            File.WriteAllText(path, sb.ToString());
        }

        public void MakeProgress()
        {
            Time = DateTime.Now;

            if (Group == 0)
            {
                Progress++;

                if (Progress >= 5)
                {
                    Group++;
                    Progress = 5;
                }
            }
            else
            {
                if (Group < 5)
                {
                    Group++;
                }
            }
        }

        public void LowerProgress()
        {
            Time = DateTime.Now;

            if (Group == 0)
            {
                Progress--;

                if (Progress < 0)
                {
                    Progress = 0;
                }
            }
            else
            {
                if (Group > 0)
                {
                    //Progress--;
                    Group--;
                }
            }
        }
    }
}
