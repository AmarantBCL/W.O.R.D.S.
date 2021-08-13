﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.Models
{
    class Wordset
    {
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

        public Wordset(int amount, Vocabulary vocabulary, Category category, bool isLearning)
        {
            startTime = DateTime.Now;
            dict = vocabulary;
            IsLearning = isLearning;

            List<int> exceptions = Setting.Exceptions;

            TimeHandler Handler = DateTime.Now.AddMinutes;

            if (category.Name == "All")
            {
                if (!IsLearning)
                {
                    Set = Word.Vocabulary.Distinct()
                        .Where(x => x.Dict.Name == vocabulary.Name)
                        .OrderBy(x => Guid.NewGuid())
                        .Take(amount)
                        .ToList();
                }
                else
                {
                    Set = Word.Vocabulary.Distinct()
                        .Where(x => x.Dict.Name == vocabulary.Name)
                        .Where(x => x.Progress >= 5 && Handler(-(x.Group * Coeff[x.Group])) > x.Time)
                        .OrderByDescending(x => x.Time)
                        .ThenBy(x => Guid.NewGuid())
                        .Union(Word.Vocabulary.Distinct()
                        .Where(x => x.Dict.Name == vocabulary.Name)
                        .Where(x => x.Progress < 5)
                        .OrderByDescending(x => x.Time > new DateTime())
                        .OrderByDescending(x => x.Progress)
                        .ThenBy(x => Guid.NewGuid())
                        ).Take(amount)
                        .OrderBy(x => Guid.NewGuid())
                        .ToList();
                }
            }
            else
            {
                if (!IsLearning)
                {
                    Set = Word.Vocabulary.Distinct()
                        .Where(x => x.Dict.Name == vocabulary.Name)
                        .Where(x => x.Category != null && x.Category.Name == category.Name)
                        .OrderBy(x => Guid.NewGuid())
                        .Take(amount)
                        .ToList();
                }
                else
                {
                    Set = Word.Vocabulary.Distinct()
                        .Where(x => x.Dict.Name == vocabulary.Name)
                        .Where(x => x.Category != null && x.Category.Name == category.Name)
                        .Where(x => x.Progress >= 5 && Handler(-(x.Group * Coeff[x.Group])) > x.Time)
                        .OrderByDescending(x => x.Time)
                        .ThenBy(x => Guid.NewGuid())
                        .Union(Word.Vocabulary.Distinct()
                        .Where(x => x.Dict.Name == vocabulary.Name)
                        .Where(x => x.Category != null && x.Category.Name == category.Name)
                        .Where(x => x.Progress < 5)
                        .OrderByDescending(x => x.Time > new DateTime())
                        .OrderByDescending(x => x.Progress)
                        .ThenBy(x => Guid.NewGuid())
                        ).Take(amount)
                        .OrderBy(x => Guid.NewGuid())
                        .ToList();
                }
            }
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

        public void CalculateResults()
        {
            TimeSpan time = (DateTime.Now - startTime);//.TotalSeconds;
            //Time = string.Format("{0:f2}", time);
            Time = time.ToString(@"hh\:mm\:ss");


            double percent = Convert.ToDouble(Right) / (Convert.ToDouble(Right) + Convert.ToDouble(Wrong)) * 10.0;
            Percent = Math.Floor(percent).ToString() + "/10";
        }

        public void SaveProgress()
        {
            Word.SaveWordsToFile(dict);
        }
    }
}
