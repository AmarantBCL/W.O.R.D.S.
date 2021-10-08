using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.Models
{
    class Flashcard : ICard
    {
        private bool reversed;

        public Word Word { get; set; }
        public string MainWord { get; set; }
        public string Translation { get; set; }
        public string Transcription { get; set; }
        public List<Option> Items { get; set; }
        public Presenter Presenter { get; set; } = new Presenter();

        public Flashcard(bool isReversed)
        {
            reversed = isReversed;
        }

        public void Show(Wordset wordset)
        {
            if (wordset.Set.Count > 0)
            {
                Word = wordset.Set[0];

                if (!reversed)
                {
                    MainWord = Word.Name;
                    Translation = Word.Translation;
                    Transcription = Word.Transcription != "" ? "[" + Word.Transcription + "]" : "";
                }
                else
                {
                    MainWord = Word.Translation;
                    Translation = Word.Name;
                    Transcription = "";
                }
            }
            else
            {
                return;
            }

            Presenter.HideAll();
            Presenter.RightWrongVisible = Visibility.Visible;
        }

        public void Answer()
        {
            Transcription = Word.Transcription != "" ? "[" + Word.Transcription + "]" : "";

            Presenter.HideAll();
            Presenter.ShowAnswerVisible = Visibility.Visible;
            Presenter.FlashcardBottomVisible = Visibility.Visible;
        }
    }
}
