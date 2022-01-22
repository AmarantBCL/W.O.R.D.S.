using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.Models
{
    class Typing : ICard
    {
        public Word Word { get; set; }
        public string MainWord { get; set; }
        public string Translation { get; set; }
        public string Transcription { get; set; }
        public List<Option> Items { get; set; }
        public Presenter Presenter { get; set; } = new Presenter();

        public void Show(Wordset wordset)
        {
            if (wordset.Set.Count > 0)
            {
                Word = wordset.Set[0];

                MainWord = Word.Translation;
                Translation = Word.Name;
                Transcription = "";
            }
            else
            {
                return;
            }

            Presenter.HideAll();
            Presenter.EnterVisible = Visibility.Visible;
            Presenter.EditVisible = Visibility.Visible;
        }

        public void Answer()
        {
            Transcription = Word.Transcription != "" ? "[" + Word.Transcription + "]" : "";

            Presenter.HideAll();
            Presenter.NextVisible = Visibility.Visible;
            Presenter.EditVisible = Visibility.Visible;
            Presenter.TypingBottomVisible = Visibility.Visible;
            Presenter.AnswerVisible = Visibility.Visible;
        }
    }
}
