using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using W.O.R.D.S.Infrastructure;
using W.O.R.D.S.Models;
using W.O.R.D.S.Models.DTO;
using W.O.R.D.S.Views;

namespace W.O.R.D.S.ViewModels
{
    class MenuViewModel : INotifyPropertyChanged
    {
        // Properties
        #region
        private Window window;

        public ObservableCollection<Level> Levels { get; set; } = new ObservableCollection<Level>
        {
            Level.A1, Level.A2, Level.B1, Level.B2, Level.C1, Level.C2, 
            Level.A, Level.B, Level.C, Level.AB, Level.BC, Level.ABC
        };

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableCollection<Vocabulary> Vocabularies { get; set; } = new ObservableCollection<Vocabulary>();

        private int totalWords;
        public int TotalWords
        {
            get => totalWords;
            set 
            { 
                totalWords = value;
                OnPropertyChanged("TotalWords");
            }
        }

        private string wordsAmount = Setting.WordsAmount.ToString();
        public string WordsAmount
        {
            get => wordsAmount;
            set
            {
                if (int.Parse(value) <= 0 || int.Parse(value) > TotalWords)
                {
                    wordsAmount = "1";
                    Setting.WordsAmount = 1;
                }
                else
                {
                    wordsAmount = value;
                    Setting.WordsAmount = int.Parse(value);
                }

                OnPropertyChanged("WordsAmount");
            }
        }

        private string gameMode;
        public string GameMode
        {
            get => gameMode;
            set
            {
                gameMode = value;
                OnPropertyChanged("GameMode");
            }
        }

        private Vocabulary selectedVocabulary;
        public Vocabulary SelectedVocabulary
        {
            get => selectedVocabulary;
            set
            {
                selectedVocabulary = value;
                OnPropertyChanged("SelectedVocabulary");

                TotalWords = Word.CountWordsInVocabulary(SelectedVocabulary);// Word.Count;
                //Setting.WordsAmount = TotalWords;
                Learning = Word.CountLearningWords(SelectedVocabulary);
                Repeat = Word.CountRepeatWords(SelectedVocabulary);
                Studied = Word.CountStudiedWords(SelectedVocabulary);
                Setting.Vocabulary = value;
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
                Setting.Category = value;
            }
        }

        private int learning;
        public int Learning
        {
            get => learning;
            set
            {
                learning = value;
                OnPropertyChanged("Learning");
            }
        }

        private int repeat;
        public int Repeat
        {
            get => repeat;
            set
            {
                repeat = value;
                OnPropertyChanged("Repeat");
            }
        }

        private int studied;
        public int Studied
        {
            get => studied;
            set
            {
                studied = value;
                OnPropertyChanged("Studied");
            }
        }
        #endregion

        // Constructor
        public MenuViewModel(Window window)
        {
            foreach (var item in Category.Read())
            {
                Categories.Add(item);

                if (item.Name == Setting.Category.Name)
                    SelectedCategory = item;
            }

            foreach (var item in Vocabulary.Read())
            {
                Vocabularies.Add(item);

                if (item.Path == Setting.Vocabulary.Path)
                    SelectedVocabulary = item;
            }

            this.window = window;
            window.Show();
        }

        // Commands
        #region
        private RelayCommand modeCommand;
        public RelayCommand ModeCommand
        {
            get
            {
                return modeCommand ??
                  (modeCommand = new RelayCommand(obj =>
                  {
                      GameMode = (string)obj;
                  }));
            }
        }

        private RelayCommand startCommand;
        public RelayCommand StartCommand
        {
            get
            {
                return startCommand ??
                  (startCommand = new RelayCommand(obj =>
                  {
                      if (GameMode == null)
                      {
                          MessageBox.Show("Выберите режим тренировки!", "Режим", MessageBoxButton.OK, MessageBoxImage.Error);

                          return;
                      }

                      if (int.TryParse(WordsAmount, out int amount))
                      {
                          if (GameMode == "6")
                          {
                              bool result = new Wordset(amount, SelectedVocabulary, SelectedCategory, true).PrepareSet();

                              if (!result)
                              {
                                  MessageBox.Show("Нет слов для тренировки!", "Слова", MessageBoxButton.OK, MessageBoxImage.Error);

                                  return;
                              }
                          }

                          new Card(GameMode, amount, SelectedVocabulary, SelectedCategory);
                          window.Close();
                      }
                      else
                      {
                          MessageBox.Show("Укажите корректное количество слов для тренировки!", "Слова", MessageBoxButton.OK, MessageBoxImage.Error);
                      }
                  }));
            }
        }

        private RelayCommand dictionaryCommand;
        public RelayCommand DictionaryCommand
        {
            get
            {
                return dictionaryCommand ??
                  (dictionaryCommand = new RelayCommand(obj =>
                  {
                      new Dictionary(SelectedVocabulary);
                      window.Close();
                  }));
            }
        }

        private RelayCommand settingsCommand;
        public RelayCommand SettingsCommand
        {
            get
            {
                return settingsCommand ??
                  (settingsCommand = new RelayCommand(obj =>
                  {
                      new Settings();
                      window.Close();
                  }));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
