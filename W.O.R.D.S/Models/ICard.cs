using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.Models
{
    interface ICard
    {
        Word Word { get; set; }
        string MainWord { get; set; }
        string Translation { get; set; }
        string Transcription { get; set; }
        List<Option> Items { get; set; }
        Presenter Presenter { get; set; }
        void Show(Wordset wordset);
        void Answer();
    }
}
