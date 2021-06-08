using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.Models
{
    class Learning : ICard
    {
        private ICard mode;

        public Word Word { get; set; }
        public string MainWord { get; set; }
        public string Translation { get; set; }
        public string Transcription { get; set; }
        public List<Option> Items { get; set; }
        public Presenter Presenter { get; set; } = new Presenter();

        public void Show(Wordset wordset)
        {
            Word word = wordset.Set[0];

            if (word.Group == 0)
            {
                switch (word.Progress)
                {
                    case -1:
                        word.SetProgress();
                        mode = new Flashcard(false);
                        break;
                    case 0:
                        mode = new Flashcard(false);
                        break;
                    case 1:
                        mode = new Flashcard(true);
                        break;
                    case 2:
                        mode = new Variants(false);
                        break;
                    case 3:
                        mode = new Variants(true);
                        break;
                    case 4:
                        mode = new Typing();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                mode = new Mixed();
            }

            mode.Show(wordset);
            CopyMode();
        }

        public void Answer()
        {
            mode.Answer();
            CopyMode();
        }

        private void CopyMode()
        {
            Word = mode.Word;
            MainWord = mode.MainWord;
            Translation = mode.Translation;
            Transcription = mode.Transcription;
            Items = mode.Items;
            Presenter = mode.Presenter;
        }
    }
}
