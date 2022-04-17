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
            Word word = (Word)value;

            if (word == null)
                return "/Icons/blank.png";

            if (word.Note != "")
            {
                string tag = ThesaurusInfo.GetTag(word.Note);

                if (ThesaurusInfo.Articles.ContainsKey(tag))
                {
                    string image = ThesaurusInfo.Articles[tag];
                    return "/Icons/" + image.ToLower() + ".png";
                }
            }

            return "/Icons/blank.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
