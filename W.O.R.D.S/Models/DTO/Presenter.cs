using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace W.O.R.D.S.Models.DTO
{
    class Presenter
    {
        public Visibility RightWrongVisible { get; set; }
        public Visibility ShowAnswerVisible { get; set; }
        public Visibility NextVisible { get; set; }
        public Visibility EnterVisible { get; set; }
        public Visibility EditVisible { get; set; }
        public Visibility FlashcardBottomVisible { get; set; }
        public Visibility VariantsBottomVisible { get; set; }
        public Visibility TypingBottomVisible { get; set; }
        public Visibility CloseVisible { get; set; }

        public void HideAll()
        {
            RightWrongVisible = Visibility.Collapsed;
            ShowAnswerVisible = Visibility.Collapsed;
            NextVisible = Visibility.Collapsed;
            EnterVisible = Visibility.Collapsed;
            EditVisible = Visibility.Collapsed;
            FlashcardBottomVisible = Visibility.Collapsed;
            VariantsBottomVisible = Visibility.Collapsed;
            TypingBottomVisible = Visibility.Collapsed;
            CloseVisible = Visibility.Collapsed;
        }
    }
}
