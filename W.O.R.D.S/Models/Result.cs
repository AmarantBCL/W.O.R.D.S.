using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.Models
{
    class Result : ICard
    {
        public Word Word { get; set; }
        public string MainWord { get; set; }
        public string Translation { get; set; }
        public string Transcription { get; set; }
        public List<Option> Items { get; set; }
        public Presenter Presenter { get; set; } = new Presenter();

        public Result()
        {
            Presenter.HideAll();
            Presenter.CloseVisible = Visibility.Visible;
        }

        public void Answer()
        {
            
        }

        public void Show(Wordset wordset)
        {
            wordset.CalculateResults();
        }
    }
}
