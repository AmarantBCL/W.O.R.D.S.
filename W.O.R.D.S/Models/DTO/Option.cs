using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace W.O.R.D.S.Models.DTO
{
    class Option : INotifyPropertyChanged
    {
        public string Text { get; set; }

        private Brush color;
        public Brush Color 
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }

        public Option(string text)
        {
            Text = text;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
