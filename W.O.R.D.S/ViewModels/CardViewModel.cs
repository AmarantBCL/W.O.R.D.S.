using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using W.O.R.D.S.Infrastructure;
using W.O.R.D.S.Models;
using W.O.R.D.S.Models.DTO;

namespace W.O.R.D.S.ViewModels
{
    class CardViewModel : INotifyPropertyChanged
    {
        // Properties
        #region
        private Window window;
        private int maxWords;

        public Wordset WordSet { get; set; }
        public ICard Mode { get; set; }

        private string answer;
        public string Answer
        {
            get => answer;
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }

        private bool elementsEnabled;
        public bool ElementsEnabled
        {
            get => elementsEnabled;
            set
            {
                elementsEnabled = value;
                OnPropertyChanged("ElementsEnabled");
            }
        }

        private string editText;
        public string EditText
        {
            get => editText;
            set
            {
                editText = value;
                OnPropertyChanged("EditText");
            }
        }

        private Option selectedVariant;
        public Option SelectedVariant
        {
            get => selectedVariant;
            set
            {
                ElementsEnabled = false;
                selectedVariant = value;
                OnPropertyChanged("SelectedVariant");

                if (selectedVariant == null)
                    return;

                Mode.Transcription = "[" + Mode.Word.Transcription + "]";
                OnPropertyChanged("Mode");

                if (selectedVariant.Text == Mode.Translation)
                {
                    selectedVariant.Color = Brushes.LightGreen;
                    Answer = "1";
                }
                else
                {
                    selectedVariant.Color = Brushes.Salmon;
                    Answer = "0";

                    foreach (var item in Mode.Items)
                    {
                        if (item.Text == Mode.Translation)
                            item.Color = Brushes.SteelBlue;
                    }
                }
            }
        }

        private string count;
        public string Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }
        #endregion

        // Constructor
        public CardViewModel(Window window, string gameMode, int wordAmount, Vocabulary vocabulary, Category category)
        {
            this.window = window;
            window.Show();

            if (gameMode == "6")
                WordSet = new Wordset(wordAmount, vocabulary, category, true);
            else
                WordSet = new Wordset(wordAmount, vocabulary, category, false);

            maxWords = WordSet.Set.Count;
            ElementsEnabled = true;

            switch (gameMode)
            {
                case "0":
                    Mode = new Flashcard(false);
                    break;
                case "1":
                    Mode = new Flashcard(true);
                    break;
                case "2":
                    Mode = new Variants(false);
                    break;
                case "3":
                    Mode = new Variants(true);
                    break;
                case "4":
                    Mode = new Typing();
                    break;
                case "5":
                    Mode = new Mixed();
                    break;
                case "6":
                    Mode = new Learning();
                    break;
                default:
                    break;
            }

            SwitchCard(false);
        }

        // Commands
        #region
        private RelayCommand answerCommand;
        public RelayCommand AnswerCommand
        {
            get
            {
                return answerCommand ??
                  (answerCommand = new RelayCommand(obj =>
                  {
                      ElementsEnabled = false;

                      if (EditText != null)
                      {
                          if (EditText.ToLower() == Mode.Translation.ToLower())
                              Answer = "1";
                          else
                          {
                              EditText = Mode.Translation;
                              Answer = "0";
                          }
                      }

                      Mode.Answer();
                      OnPropertyChanged("Mode");
                  }));
            }
        }

        private RelayCommand nextCommand;
        public RelayCommand NextCommand
        {
            get
            {
                return nextCommand ??
                  (nextCommand = new RelayCommand(obj =>
                  {
                      if (obj == null)
                          WordSet.Remove(Answer);
                      else
                          WordSet.Remove(obj.ToString());

                      if (WordSet.Set.Count > 0)
                      {
                          SwitchCard(false);
                          Answer = "0";
                          ElementsEnabled = true;
                          EditText = "";
                      }
                      else
                      {
                          Mode = new Result();
                          SwitchCard(true);
                      }
                  }));
            }
        }

        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                  (closeCommand = new RelayCommand(obj =>
                  {
                        WordSet.SaveProgress();
                        new MenuViewModel(new MainWindow());
                        window.Close();
                  }));
            }
        }
        #endregion

        // Methods
        #region
        private void SwitchCard(bool isResult)
        {
            Mode.Show(WordSet);

            if (isResult)
            {
                Count = "";
                OnPropertyChanged("WordSet");
            }
            else
            {
                Count = $"{WordSet.Set.Count}/{maxWords}";
            }

            OnPropertyChanged("Mode");
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
