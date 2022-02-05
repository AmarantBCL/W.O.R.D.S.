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
        public ICard Mode { get; set; }

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
                        word.MakeProgress();
                        Mode = new Flashcard(false);
                        break;
                    case 0:
                        Mode = new Flashcard(false);
                        break;
                    case 1:
                        Mode = new Flashcard(true);
                        break;
                    case 2:
                        Mode = new Variants(false);
                        break;
                    case 3:
                        Mode = new Variants(true);
                        break;
                    case 4:
                        Mode = new Typing();
                        break;
                    case 5:
                        Mode = new Context();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Mode = new Mixed();
            }

            Mode.Show(wordset);
            CopyMode();
        }

        public void Answer()
        {
            Mode.Answer();
            CopyMode();
        }

        private void CopyMode()
        {
            Word = Mode.Word;
            MainWord = Mode.MainWord;
            Translation = Mode.Translation;
            Transcription = Mode.Transcription;
            Items = Mode.Items;
            Presenter = Mode.Presenter;
        }
    }
}
