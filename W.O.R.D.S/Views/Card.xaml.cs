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
using W.O.R.D.S.ViewModels;
using W.O.R.D.S.Models;

namespace W.O.R.D.S.Views
{
    /// <summary>
    /// Логика взаимодействия для Card.xaml
    /// </summary>
    public partial class Card : Window
    {
        public Card(string gameMode, int wordAmount, Category category)
        {
            InitializeComponent();
            DataContext = new CardViewModel(this, gameMode, wordAmount, category);
        }
    }
}
