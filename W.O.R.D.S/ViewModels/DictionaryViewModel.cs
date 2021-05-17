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

                    foreach (var word in Word.Vocabulary)
                    {
                        if (word.Name.Contains(search) || word.Translation.Contains(search))
                            Dictionary.Add(word);
                    }
                }
                else
                {
                    Dictionary = new ObservableCollection<Word>();

                    foreach (var word in Word.Vocabulary)
                    {
                        Dictionary.Add(word);
                    }
                }

                OnPropertyChanged("Dictionary");
                OnPropertyChanged("Search");
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
                OnPropertyChanged("WordName");
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
        #endregion

        public DictionaryViewModel(Window window)
        {
            this.window = window;
            window.Show();

            Dictionary = new ObservableCollection<Word>();

            foreach (var word in Word.Vocabulary)
            {
                Dictionary.Add(word);
            }

            foreach (var item in Category.Read())
            {
                Categories.Add(item);
            }

            SelectedCategory = Categories[1];
        }

        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                  (closeCommand = new RelayCommand(obj =>
                  {
                      Word.SaveWordsToFile();

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
                      {
                          SelectedWord.Name = WordName;
                      }

                      if (WordTranslation != null && WordTranslation != "")
                      {
                          SelectedWord.Translation = WordTranslation;
                      }

                      SelectedWord.Category = SelectedCategory;
                      Search = Search;

                      MessageBox.Show(SelectedCategory.Name);
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
