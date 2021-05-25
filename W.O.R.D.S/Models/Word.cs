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
        private static bool alreadyRead = false;

        public int Id { get; private set; }
        public string Name { get; set; }
        public string Translation { get; set; }
        public PartOfSpeech PartOfSpeech { get; private set; }
        public Level Level { get; private set; }
        public string Transcription { get; private set; }
        public string Meaning { get; private set; }
        public Category Category { get; set; }
        public Example Example { get; set; }
        public int Group { get; private set; }
        public int Progress { get; private set; }
        public DateTime Time { get; private set; }
        public static List<Word> Vocabulary { get; private set; } = new List<Word>();
        public static int Count { get; private set; } = 0;
        public Vocabulary Dict { get; set; }

        public Word(string name, string translation, PartOfSpeech partOfSpeech, Level level, string transcription, string meaning, Category category, Example example, int stage, int progress, DateTime time)
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
            var result = Vocabulary.Where(x => x.Dict.Name == vocabulary.Name)
                .ToList();

            return result;
        }

        public static int CountWordsInVocabulary(Vocabulary vocabulary)
        {
            var result = Vocabulary.Where(x => x.Dict.Name == vocabulary.Name)
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
    }
}
