using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using W.O.R.D.S.Infrastructure;
using W.O.R.D.S.Models;

namespace W.O.R.D.S.ViewModels
{
    public class EditViewModel : INotifyPropertyChanged
    {
        private readonly Window window;

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

        public ObservableCollection<Level> Levels { get; set; } = new ObservableCollection<Level>
        {
            Level.A1, Level.A2, Level.B1, Level.B2, Level.C1, Level.C2
        };

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        private int[] indexator = new int[4];
        public int[] Indexator
        {
            get => indexator;
            set
            {
                indexator = value;
                OnPropertyChanged("Indexator");
            }
        }

        public EditViewModel(Window window, Word word)
        {
            this.window = window;
            this.word = word;

            foreach (Category item in Category.Read())
            {
                Categories.Add(item);
                if (word.Category.Name == item.Name)
                {
                    SelectedCategory = item;
                }
            }

            SelectedPartOfSpeech = word.PartOfSpeech;
            SelectedLevel = word.Level;
            Indexator[0] = word.Example.Indexes[0, 0];
            Indexator[1] = word.Example.Indexes[0, 1];
            Indexator[2] = word.Example.Indexes[1, 0];
            Indexator[3] = word.Example.Indexes[1, 1];
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
                      int[,] indexes = { { Indexator[0], Indexator[1] }, { Indexator[2], Indexator[3] } };
                      word.Example.Indexes = indexes;
                      word.Example.FormatExample();
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
