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
    public class Vocabulary
    {
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

                            if (word.Example != null && word.Example.Indexes != null)
                            {
                                word.Example.FormatExample();
                            }
                        }
                    }
                }
            }

            isAlreadyRead = true;

            return list;
        }
    }
}
