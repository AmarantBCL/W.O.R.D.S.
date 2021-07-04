﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace W.O.R.D.S.Models.DTO
{
    class Presenter
    {
        public Visibility RightWrongVisible { get; set; } = Visibility.Collapsed;
        public Visibility ShowAnswerVisible { get; set; } = Visibility.Collapsed;
        public Visibility NextVisible { get; set; } = Visibility.Collapsed;
        public Visibility EnterVisible { get; set; } = Visibility.Collapsed;
        public Visibility EditVisible { get; set; } = Visibility.Collapsed;
        public Visibility FlashcardBottomVisible { get; set; } = Visibility.Collapsed;
        public Visibility VariantsBottomVisible { get; set; } = Visibility.Collapsed;
        public Visibility TypingBottomVisible { get; set; } = Visibility.Collapsed;
        public Visibility CloseVisible { get; set; } = Visibility.Collapsed;

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
