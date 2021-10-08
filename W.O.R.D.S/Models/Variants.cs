using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.Models
{
    class Variants : ICard
    {
        private bool reversed;
        public Word Word { get; set; }
        public string MainWord { get; set; }
        public string Translation { get; set; }
        public string Transcription { get; set; }
        public List<Option> Items { get; set; }
        public Presenter Presenter { get; set; } = new Presenter();

        public Variants(bool isReversed)
        {
            reversed = isReversed;
        }

        public void Show(Wordset wordset)
        {
            if (wordset.Set.Count > 0)
            {
                Word = wordset.Set[0];
                Items = new List<Option>();

                if (!reversed)
                {
                    MainWord = Word.Name;
                    Translation = Word.Translation;
                    Transcription = Word.Transcription != "" ? "[" + Word.Transcription + "]" : "";

                    var result = wordset.GenerateVariants(Word);

                    foreach (var item in result)
                    {
                        Items.Add(new Option(item.Translation));
                    }
                }
                else
                {
                    MainWord = Word.Translation;
                    Translation = Word.Name;
                    Transcription = "";

                    var result = wordset.GenerateVariants(Word);

                    foreach (var item in result)
                    {
                        Items.Add(new Option(item.Name));
                    }
                }
            }
            else
            {
                return;
            }

            Presenter.HideAll();
            Presenter.NextVisible = Visibility.Visible;
            Presenter.VariantsBottomVisible = Visibility.Visible;
        }

        public void Answer()
        {
            throw new NotImplementedException();
        }
    }
}
