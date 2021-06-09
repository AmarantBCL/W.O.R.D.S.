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
        public bool IsLearning { get; set; } = false;

        public Wordset(int amount, Vocabulary vocabulary, Category category, bool isLearning)
        {
            startTime = DateTime.Now;
            dict = vocabulary;
            IsLearning = isLearning;

            List<int> exceptions = Setting.Exceptions;

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
                        .OrderBy(x => Guid.NewGuid())
                        .Take(amount)
                        .OrderBy(x => x.Group)
                        .OrderBy(x => x.Progress)
                        .ToList();
                }
            }
            else
            {
                Set = Word.Vocabulary.Distinct()
                    .Where(x => x.Dict.Name == vocabulary.Name)
                    .Where(x => x.Category != null && x.Category.Name == category.Name)
                    .OrderBy(x => Guid.NewGuid())
                    .Take(amount)
                    .ToList();
            }

            string text = "";

            foreach (var item in Set)
            {
                text += $"{item.Name}, G: {item.Group} - P: {item.Progress}\n";
            }

            MessageBox.Show(text);
        }

        public void Remove(string correct)
        {
            if (correct == "1")
            {
                Right++;

                if (IsLearning)
                {
                    Set[0].SetProgress();
                }
            }
            else
                Wrong++;

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

        public void CalculateTime()
        {
            double time = (DateTime.Now - startTime).TotalSeconds;
            Time = string.Format("{0:f2}", time);
        }
    }
}
