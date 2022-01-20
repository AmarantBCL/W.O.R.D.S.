using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Infrastructure;
using W.O.R.D.S.Models;
using W.O.R.D.S.Views;

namespace W.O.R.D.S.ViewModels
{
    class EditViewModel : INotifyPropertyChanged
    {
        private Window window;

        private Word word;
        public Word Word
        {
            get => word;
            set
            {
                word = value;
                OnPropertyChanged("Word");
            }
        }

        private PartOfSpeech selectedPartOfSpeech;
        public PartOfSpeech SelectedPartOfSpeech
        {
            get => selectedPartOfSpeech;
            set
            {
                selectedPartOfSpeech = value;
                OnPropertyChanged("SelectedPartOfSpeech");
            }
        }

        private Level selectedLevel;
        public Level SelectedLevel
        {
            get => selectedLevel;
            set
            {
                selectedLevel = value;
                OnPropertyChanged("SelectedLevel");
            }
        }

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        public ObservableCollection<PartOfSpeech> PartsOfSpeech { get; set; } = new ObservableCollection<PartOfSpeech>(
            Enum.GetValues(typeof(PartOfSpeech)).Cast<PartOfSpeech>().ToList());

        public ObservableCollection<Level> Levels { get; set; } = new ObservableCollection<Level>(
            Enum.GetValues(typeof(Level)).Cast<Level>().ToList());

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public EditViewModel(Window window, Word word)
        {
            this.window = window;
            this.word = word;

            foreach (var item in Category.Read())
            {
                Categories.Add(item);
                if (word.Category.Name == item.Name)
                {
                    SelectedCategory = item;
                }
            }

            SelectedPartOfSpeech = word.PartOfSpeech;
            SelectedLevel = word.Level;
        }

        private RelayCommand okCommand;
        public RelayCommand OKCommand
        {
            get
            {
                return okCommand ??
                  (okCommand = new RelayCommand(obj =>
                  {
                      word.PartOfSpeech = SelectedPartOfSpeech;
                      word.Level = SelectedLevel;
                      word.Category = SelectedCategory;
                      Word.SaveWordsToFile(word.Dict);
                      MessageBox.Show($"Слово {word.Name} было успешно отредактировано!");
                      window.Close();
                  }));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                  (cancelCommand = new RelayCommand(obj =>
                  {
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
