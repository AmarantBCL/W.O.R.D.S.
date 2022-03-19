using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using W.O.R.D.S.Models;
using W.O.R.D.S.ViewModels;

namespace W.O.R.D.S.Views
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public Edit(Word word, Example example)
        {
            InitializeComponent();
            DataContext = new EditViewModel(this, word, example);
        }
    }
}
