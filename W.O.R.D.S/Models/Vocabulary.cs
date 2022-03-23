using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.Models
{
    public class Vocabulary
    {
        private static Dictionary<string, PartOfSpeech> PART_OF_SPEECH_KEYS = new Dictionary<string, PartOfSpeech>
        {
            { "n", PartOfSpeech.Noun },
            { "a", PartOfSpeech.Adjective },
            { "v", PartOfSpeech.Verb },
            { "adv", PartOfSpeech.Adverb },
            { "pron", PartOfSpeech.Pronoun },
            { "prep", PartOfSpeech.Preposition },
            { "conj", PartOfSpeech.Conjunction },
            { "COL", PartOfSpeech.Collocation },
            { "SENT", PartOfSpeech.Sentence },
            { "PHR", PartOfSpeech.Phrasal },
            { "IDIOM", PartOfSpeech.Idiom },
        };

        private static bool isAlreadyRead = false;
        public string Path { get; set; }
        public string Name { get; set; }
        public static Vocabulary Fav {get; set; }

        public Vocabulary(string name)
        {
            Path = name;
            Name = name.Substring(0, name.Length - 4);
        }

        public static List<Vocabulary> Read()
        {
            string path = @"Files/Dictionaries/";

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] fileInfos = dirInfo.GetFiles("*.txt");

            List<Vocabulary> list = new List<Vocabulary>();

            foreach (var file in fileInfos)
            {
                Vocabulary vocab = new Vocabulary(file.Name);
                list.Add(vocab);

                if (!isAlreadyRead)
                {
                    string wordsPath = @"Files/Dictionaries/" + file.Name;
                    string[] lines = File.ReadAllLines(wordsPath);

                    if (file.Name == "Favorites.txt")
                    {
                        Fav = vocab;
                    }

                    foreach (var item in lines)
                    {
                        if (item != "")
                        {
                            Word word = JsonConvert.DeserializeObject<Word>(item);
                            word.Dict = vocab;

                            foreach (var example in word.Examples)
                            {
                                if (example != null && example.Indexes != null)
                                {
                                    example.FormatExample();
                                }
                            }
                        }
                    }
                }
            }

            isAlreadyRead = true;

            return list;
        }

        public static void CheckNewWords(Vocabulary vocabulary)
        {
            string path = @"Files/NEW_WORDS.txt";
            string pattern = @"(.+)\s-\s(.+)\s\[(.?|.+)\]\s\[(\w+),\s(\w+\d+),\s(\w+)\]\s\[(.?|.+)\]\s\[(.?|.+)\]";
            string[] lines = File.ReadAllLines(path);

            // In Debug/NEW_WORDS.txt
            // WORD - TRANSLATION [TRANSCRIPTION] [PART_OF_SPEECH_KEY, LEVEL, CATEGORY] [MEANING] [EXAMPLE]

            List<Category> categories = new List<Category>();
            foreach (var item in Category.Read())
            {
                categories.Add(item);
            }

            List<string> newLines = new List<string>();

            Regex regex = new Regex(pattern);
            foreach (var line in lines)
            {
                MatchCollection matches = regex.Matches(line);
                if (matches != null && matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        string name = match.Groups[1].Value;
                        string translation = match.Groups[2].Value;
                        string transcription = match.Groups[3].Value;
                        string partOfSpeech = match.Groups[4].Value;
                        string level = match.Groups[5].Value;
                        string category = match.Groups[6].Value;
                        string meaning = match.Groups[7].Value;
                        string examples = match.Groups[8].Value;

                        Category cat = new Category("All");
                        foreach (var item in categories)
                        {
                            if (item.Name == category)
                            {
                                cat = item;
                            }
                        }

                        List<Example> exampleList = new List<Example>();

                        Word word = new Word(name, translation, PART_OF_SPEECH_KEYS[partOfSpeech], (Level)Enum.Parse(typeof(Level), 
                            level), transcription, meaning, cat, exampleList, "", 0, -1, new DateTime(), false);
                        word.Dict = vocabulary;
                        //word.Example.Index(name); MUST BE INDEXED!
                    }
                }
                else
                {
                    newLines.Add(line);
                }
            }

            File.WriteAllLines(path, newLines.ToArray());
            GenerateForWordFinder();
        }

        public static void GenerateForWordFinder()
        {
            string path = @"D:/WordList.txt";
            StringBuilder sb = new StringBuilder();
            foreach (var word in Word.Vocabulary)
            {
                if (word.Dict.Name == "MERGED VOCABULARY" && word.WordClass == WordClass.Word)
                    sb.AppendLine(word.Name);
            }
            File.WriteAllText(path, sb.ToString());
        }
    }
}
