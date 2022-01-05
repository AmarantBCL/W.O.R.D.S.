using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using W.O.R.D.S.Models.Info;

namespace W.O.R.D.S.Models.Converters
{
    class NoteImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string key = value.ToString();
            if (Thesaurus.Article.ContainsKey(key))
            {
                string image = Thesaurus.Article[key];
                return "/Icons/" + image.ToLower() + ".png";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
