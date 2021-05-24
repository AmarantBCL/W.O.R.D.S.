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
                Setting.Order = value;
                showOrder = value;
                OnPropertyChanged("ShowOrder");
            }
        }

        public SettingsViewModel(Window window)
        {
            showOrder = Setting.Order;

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
