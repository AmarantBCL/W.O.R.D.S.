using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace W.O.R.D.S.Models.DTO
{
    public class Presenter
    {
        public Visibility RightWrongVisible { get; set; } = Visibility.Collapsed;
        public Visibility ShowAnswerVisible { get; set; } = Visibility.Collapsed;
        public Visibility NextVisible { get; set; } = Visibility.Collapsed;
        public Visibility EnterVisible { get; set; } = Visibility.Collapsed;
        public Visibility EditVisible { get; set; } = Visibility.Collapsed;
        public Visibility SecondEditVisible { get; set; } = Visibility.Collapsed;
        public Visibility FlashcardBottomVisible { get; set; } = Visibility.Collapsed;
        public Visibility VariantsBottomVisible { get; set; } = Visibility.Collapsed;
        public Visibility TypingBottomVisible { get; set; } = Visibility.Collapsed;
        public Visibility CloseVisible { get; set; } = Visibility.Collapsed;
        public Visibility ContextVisible { get; set; } = Visibility.Collapsed;
        public Visibility AnswerVisible { get; set; } = Visibility.Hidden;
        public Visibility ShowOrderVisible { get; set; } = Visibility.Collapsed;
        public Visibility ShowCategoryVisible { get; set; } = Visibility.Collapsed;
        public Visibility ShowLevelVisible { get; set; } = Visibility.Collapsed;
        public Visibility ShowPartOfSpeechVisible { get; set; } = Visibility.Collapsed;
        public Visibility ShowTranscriptionVisible { get; set; } = Visibility.Collapsed;
        public Visibility ShowMeaningVisible { get; set; } = Visibility.Hidden;
        public Visibility ShowExampleVisible { get; set; } = Visibility.Hidden;
        public Visibility StarVisible { get; set; } = Visibility.Visible;

        public void HideAll()
        {
            RightWrongVisible = Visibility.Collapsed;
            ShowAnswerVisible = Visibility.Collapsed;
            NextVisible = Visibility.Collapsed;
            EnterVisible = Visibility.Collapsed;
            EditVisible = Visibility.Collapsed;
            SecondEditVisible = Visibility.Collapsed;
            FlashcardBottomVisible = Visibility.Collapsed;
            VariantsBottomVisible = Visibility.Collapsed;
            TypingBottomVisible = Visibility.Collapsed;
            CloseVisible = Visibility.Collapsed;
            ContextVisible = Visibility.Collapsed;
            AnswerVisible = Visibility.Hidden;
            ShowOrderVisible = Visibility.Collapsed;
            ShowCategoryVisible = Visibility.Collapsed;
            ShowLevelVisible = Visibility.Collapsed;
            ShowPartOfSpeechVisible = Visibility.Collapsed;
            ShowTranscriptionVisible = Visibility.Collapsed;
            ShowMeaningVisible = Visibility.Hidden;
            ShowExampleVisible = Visibility.Hidden;
            StarVisible = Visibility.Visible;

            AdjustSettings();
        }

        public void AdjustSettings()
        {
            if (Setting.ShowOrder)
                ShowOrderVisible = Visibility.Visible;

            if (Setting.ShowCategory)
                ShowCategoryVisible = Visibility.Visible;

            if (Setting.ShowLevel)
                ShowLevelVisible = Visibility.Visible;

            if (Setting.ShowPartOfSpeech)
                ShowPartOfSpeechVisible = Visibility.Visible;

            if (Setting.ShowTranscription)
                ShowTranscriptionVisible = Visibility.Visible;

            if (Setting.ShowMeaning)
               ShowMeaningVisible = Visibility.Visible;

            if (Setting.ShowExamples)
                ShowExampleVisible = Visibility.Visible;
        }
    }
}
