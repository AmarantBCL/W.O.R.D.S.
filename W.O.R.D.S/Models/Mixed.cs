using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.Models
{
    class Mixed : ICard
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
            Random random = new Random();
            int next = random.Next(5);

            switch (next)
            {
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
