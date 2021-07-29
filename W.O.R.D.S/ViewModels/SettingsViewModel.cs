using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Infrastructure;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private Window window;

        private bool showOrder;
        public bool ShowOrder
        {
            get => showOrder;
            set
            {
                Setting.ShowOrder = value;
                showOrder = value;
                OnPropertyChanged("ShowOrder");
            }
        }

        private bool showCategory;
        public bool ShowCategory
        {
            get => showCategory;
            set
            {
                Setting.ShowCategory = value;
                showCategory = value;
                OnPropertyChanged("ShowCategory");
            }
        }

        private bool showLevel;
        public bool ShowLevel
        {
            get => showLevel;
            set
            {
                Setting.ShowLevel = value;
                showLevel = value;
                OnPropertyChanged("ShowLevel");
            }
        }

        private bool showPartOfSpeech;
        public bool ShowPartOfSpeech
        {
            get => showPartOfSpeech;
            set
            {
                Setting.ShowPartOfSpeech = value;
                showPartOfSpeech = value;
                OnPropertyChanged("ShowPartOfSpeech");
            }
        }

        private bool showTranscription;
        public bool ShowTranscription
        {
            get => showTranscription;
            set
            {
                Setting.ShowTranscription = value;
                showTranscription = value;
                OnPropertyChanged("ShowTranscription");
            }
        }

        private bool showMeaning;
        public bool ShowMeaning
        {
            get => showMeaning;
            set
            {
                Setting.ShowMeaning = value;
                showMeaning = value;
                OnPropertyChanged("ShowMeaning");
            }
        }

        private bool showExamples;
        public bool ShowExamples
        {
            get => showExamples;
            set
            {
                Setting.ShowExamples = value;
                showExamples = value;
                OnPropertyChanged("ShowExamples");
            }
        }

        public SettingsViewModel(Window window)
        {
            showOrder = Setting.ShowOrder;
            showCategory = Setting.ShowCategory;
            showLevel = Setting.ShowLevel;
            showPartOfSpeech = Setting.ShowPartOfSpeech;
            showTranscription = Setting.ShowTranscription;
            showMeaning = Setting.ShowMeaning;
            showExamples = Setting.ShowExamples;

            this.window = window;
            window.Show();
        }


        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      new MenuViewModel(new MainWindow());
                      window.Close();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
