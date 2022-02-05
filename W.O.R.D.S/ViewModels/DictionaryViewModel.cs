using Newtonsoft.Json;
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

namespace W.O.R.D.S.ViewModels
{
    class DictionaryViewModel : INotifyPropertyChanged
    {
        // Properties
        #region
        private Window window;
        private Vocabulary vocabulary;

        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public ObservableCollection<Word> Dictionary { get; set; }

        private string search;
        public string Search
        {
            get => search;
            set
            {
                search = value;

                if (search != null && search != "")
                {
                    Dictionary = new ObservableCollection<Word>();

                    foreach (var word in Word.GetWordsFromVocabulary(vocabulary))
                    {
                        if (word.Name.Contains(search) || word.Translation.Contains(search))
                            Dictionary.Add(word);
                    }
                }
                else
                {
                    Dictionary = new ObservableCollection<Word>();

                    foreach (var word in Word.GetWordsFromVocabulary(vocabulary))
                    {
                        Dictionary.Add(word);
                    }
                }

                OnPropertyChanged("Dictionary");
                OnPropertyChanged("Search");
                ExNumber = 0;
            }
        }

        private Word selectedWord;
        public Word SelectedWord
        {
            get => selectedWord;
            set
            {
                selectedWord = value;
                OnPropertyChanged("SelectedWord");
            }
        }

        private string wordName;
        public string WordName
        {
            get => wordName;
            set
            {
                wordName = value;
                Search = value;
                OnPropertyChanged("WordName");
                OnPropertyChanged("Search");
            }
        }

        private string wordTranslation;
        public string WordTranslation
        {
            get => wordTranslation;
            set
            {
                wordTranslation = value;
                OnPropertyChanged("WordTranslation");
            }
        }

        private string wordTranscription = "";
        public string WordTranscription
        {
            get => wordTranscription;
            set
            {
                wordTranscription = value;
                OnPropertyChanged("WordTranscription");
            }
        }

        private string wordMeaning = "";
        public string WordMeaning
        {
            get => wordMeaning;
            set
            {
                wordMeaning = value;
                OnPropertyChanged("WordMeaning");
            }
        }

        private string wordExample;
        public string WordExample
        {
            get => wordExample;
            set
            {
                wordExample = value;
                OnPropertyChanged("WordExample");
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

        public ObservableCollection<Level> Levels { get; set; } = new ObservableCollection<Level>
        {
            Level.A1, Level.A2, Level.B1, Level.B2, Level.C1, Level.C2
        };

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

        public ObservableCollection<PartOfSpeech> PartsOfSpeech { get; set; } = new ObservableCollection<PartOfSpeech>
        {
            PartOfSpeech.Noun, PartOfSpeech.Adjective, PartOfSpeech.Verb, PartOfSpeech.Adverb, PartOfSpeech.Preposition,
            PartOfSpeech.Conjunction, PartOfSpeech.Pronoun,
            PartOfSpeech.Collocation, PartOfSpeech.Sentence, PartOfSpeech.Phrasal, PartOfSpeech.Idiom
        };

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

        private bool noLevel = true;
        public bool NoLevel
        {
            get => noLevel;
            set
            {
                noLevel = value;
                OnPropertyChanged("NoLevel");
            }
        }

        private bool noCategory = true;
        public bool NoCategory
        {
            get => noCategory;
            set
            {
                noCategory = value;
                OnPropertyChanged("NoCategory");
            }
        }

        private bool noPartOfSpeech = true;
        public bool NoPartOfSpeech
        {
            get => noPartOfSpeech;
            set
            {
                noPartOfSpeech = value;
                OnPropertyChanged("NoPartOfSpeech");
            }
        }

        private bool oxfordCambridge = true;
        public bool OxfordCambridge
        {
            get => oxfordCambridge;
            set
            {
                oxfordCambridge = value;
                OnPropertyChanged("OxfordCambridge");
            }
        }

        private bool noExamples = false;
        public bool NoExamples
        {
            get => noExamples;
            set
            {
                noExamples = value;

                if (!noExamples)
                {
                    Dictionary = new ObservableCollection<Word>();

                    foreach (var word in Word.GetWordsFromVocabulary(vocabulary))
                    {
                        Dictionary.Add(word);
                    }
                }
                else
                {
                    Dictionary = new ObservableCollection<Word>();

                    foreach (var word in Word.GetWordsFromVocabulary(vocabulary))
                    {
                        if (word.Example != null && word.Example.Name == " " || word.Example.Name == "")
                            Dictionary.Add(word);
                    }
                }

                OnPropertyChanged("Dictionary");
                OnPropertyChanged("NoExamples");
            }
        }

        private int exNumber = 0;
        public int ExNumber
        {
            get => exNumber;
            set
            {
                var result = Word.GetWordsFromVocabulary(vocabulary)
                    .Where(x => x.Example == null || (x.Example != null && (x.Example.Name == " " || x.Example.Name == "")) && x.PartOfSpeech != PartOfSpeech.Sentence)
                    .Count();

                exNumber = result;
                OnPropertyChanged("ExNumber");
            }
        }
        #endregion

        public DictionaryViewModel(Window window, Vocabulary vocabulary)
        {
            this.window = window;
            this.vocabulary = vocabulary;
            window.Show();

            Dictionary = new ObservableCollection<Word>();

            foreach (var word in Word.GetWordsFromVocabulary(vocabulary))
            {
                Dictionary.Add(word);
            }

            foreach (var item in Category.Read())
            {
                Categories.Add(item);
            }

            SelectedCategory = Categories[1];
            ExNumber = 0;
        }

        private void SetOxfordCambridgeFormat(Word word, Vocabulary vocabulary)
        {
            if (OxfordCambridge) // LAME. DELETE AFTERWARDS.
            {
                string result = WordMeaning;

                if (word.PartOfSpeech == PartOfSpeech.Noun)
                {
                    word.Meaning = $"A {word.Name} is {result}.";
                }
                else if (word.PartOfSpeech == PartOfSpeech.Verb)
                {
                    Random random = new Random();
                    int a = random.Next(1);

                    string divider = a == 0 ? "is" : "means";

                    word.Meaning = $"To {word.Name} {divider} {result}.";
                }
                else
                {
                    string firstLetter = word.Name.Substring(0, 1).ToUpper();
                    string restOfTheWord = word.Name.Substring(1);

                    word.Meaning = $"{firstLetter + restOfTheWord} is {result}.";
                }
            }
            else
            {
                word.Meaning = WordMeaning;
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
                      Word.SaveWordsToFile(vocabulary);

                      new MenuViewModel(new MainWindow());
                      window.Close();
                  }));
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(obj =>
                  {
                      if (SelectedWord == null)
                          return;

                      if (WordName != null && WordName != "")
                         SelectedWord.Name = WordName;

                      if (WordTranslation != null && WordTranslation != "")
                         SelectedWord.Translation = WordTranslation;

                      if (!NoLevel)
                         SelectedWord.Level = SelectedLevel;

                      if (!NoCategory)
                         SelectedWord.Category = SelectedCategory;

                      if (!NoPartOfSpeech)
                        SelectedWord.PartOfSpeech = SelectedPartOfSpeech;

                      if (WordTranscription != null && WordTranscription != "")
                        SelectedWord.Transcription = WordTranscription;

                      if (WordMeaning != null && WordMeaning != "")
                      {
                          SetOxfordCambridgeFormat(SelectedWord, vocabulary);
                      }  

                      if (WordExample != null && WordExample != "")
                          SelectedWord.Example = new Example(WordExample);

                      WordName = "";
                      WordTranslation = "";
                      WordTranscription = "";
                      WordMeaning = "";
                      WordExample = "";
                      Search = Search;

                      //MessageBox.Show($"Слово {WordName} успешно отредактировано.", "Новое слово", MessageBoxButton.OK, MessageBoxImage.Information);
                  }));
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      if (WordName != "" && WordName != null && WordTranslation != "" && WordTranslation != null)
                      {
                          bool isAlreadyInVocab = Word.IsAlreadyInVocabulary(WordName, vocabulary.Name);

                          if (isAlreadyInVocab)
                          {
                              MessageBox.Show($"Слово {WordName} уже есть в словаре!", "Новое слово", MessageBoxButton.OK, MessageBoxImage.Error);
                          }

                          Word word = new Word(WordName, WordTranslation, SelectedPartOfSpeech, SelectedLevel, WordTranscription, WordMeaning, SelectedCategory, new Example(WordExample), "", 0, -1, new DateTime(), false);
                          word.Dict = vocabulary;
                          word.Example.Index(WordName);

                          SetOxfordCambridgeFormat(word, vocabulary);

                          //MessageBox.Show($"Слово {WordName} успешно добавлено.", "Новое слово", MessageBoxButton.OK, MessageBoxImage.Information);

                          WordName = "";
                          WordTranslation = "";
                          WordTranscription = "";
                          WordMeaning = "";
                          WordExample = "";
                          Search = "";
                      }
                      else
                      {
                          MessageBox.Show("Укажите слово и перевод!", "Новое слово", MessageBoxButton.OK, MessageBoxImage.Error);
                      }
                  }));
            }
        }

        private RelayCommand resetCommand;
        public RelayCommand ResetCommand
        {
            get
            {
                return resetCommand ??
                  (resetCommand = new RelayCommand(obj =>
                  {
                      if (SelectedWord == null) // FOR THE ONE WORD
                          return;

                      //SelectedWord.Group = 0;
                      //SelectedWord.Progress = -1;

                      Word.DeleteWordFromVocabulary(SelectedWord, vocabulary);
                      SelectedWord = null;

                      //foreach (var item in Dictionary) // FOR ALL THE WORDS
                      //{
                      //    item.Group = 0;
                      //    item.Progress = -1;
                      //    item.Time = new DateTime();
                      //}

                      Search = "";

                      //MessageBox.Show($"Прогресс по слову {WordName} был успешно сброшен.", "Прогресс слова", MessageBoxButton.OK, MessageBoxImage.Information);
                      MessageBox.Show($"Слово {WordName} было успешно удалено.", "Удаление слова", MessageBoxButton.OK, MessageBoxImage.Information);
                  }));
            }
        }

        private RelayCommand testCommand;
        public RelayCommand TestCommand
        {
            get
            {
                return testCommand ??
                  (testCommand = new RelayCommand(obj =>
                  {
                      foreach (var word in Dictionary)
                      {
                          word.Example.Index(word.Name);
                          word.Group = 0;
                          word.Progress = -1;
                          word.Time = new DateTime();

                          if (word.Translation == "n")
                          {
                              word.PartOfSpeech = PartOfSpeech.Noun;
                          }
                          else if (word.Translation == "a")
                          {
                              word.PartOfSpeech = PartOfSpeech.Adjective;
                          }
                          else if (word.Translation == "v")
                          {
                              word.PartOfSpeech = PartOfSpeech.Verb;
                          }
                          else if (word.Translation == "adv")
                          {
                              word.PartOfSpeech = PartOfSpeech.Adverb;
                          }
                          else if (word.Translation == "pron")
                          {
                              word.PartOfSpeech = PartOfSpeech.Pronoun;
                          }
                          else if (word.Translation == "prep")
                          {
                              word.PartOfSpeech = PartOfSpeech.Preposition;
                          }
                          else if (word.Translation == "conj")
                          {
                              word.PartOfSpeech = PartOfSpeech.Conjunction;
                          }
                          else if (word.Translation == "COL")
                          {
                              word.PartOfSpeech = PartOfSpeech.Collocation;
                          }
                          else if (word.Translation == "PHRASAL")
                          {
                              word.PartOfSpeech = PartOfSpeech.Phrasal;
                          }
                          else if (word.Translation == "SENT")
                          {
                              word.PartOfSpeech = PartOfSpeech.Sentence;
                          }
                      }

                      MessageBox.Show("Все примеры были успешно проиндексированы.", "Индексация примеров", MessageBoxButton.OK, MessageBoxImage.Information);
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
