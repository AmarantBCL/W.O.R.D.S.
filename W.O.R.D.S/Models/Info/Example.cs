using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace W.O.R.D.S.Models
{
    public class Example
    {
        public string Name { get; set; }
        public int[,] Indexes { get; set; } = { { 0, 0 }, { 0, 0 } };
        public string Left { get; set; }
        public string Main { get; set; }
        public string Right { get; set; }
        public string Second { get; set; }
        public string Final { get; set; }

        public Example(string name)
        {
            if (name == null || name == "")
            {
                name = " ";
            }

            Name = name;
        }

        public void FormatExample()
        {
            if (Name == " ")
            {
                Left = "";
                Main = "";
                Right = "";
                Second = "";
                Final = "";

                return;
            }

            if (Indexes[1, 0] == 0 && Indexes[1, 1] == 0)
            {
                Left = Name.Substring(0, Indexes[0, 0]);
                Main = Name.Substring(Indexes[0, 0], Indexes[0, 1] - Indexes[0, 0] + 1);
                Right = Name.Substring(Indexes[0, 1] + 1);
                Second = "";
                Final = "";
            }
            else
            {
                Left = Name.Substring(0, Indexes[0, 0]);
                Main = Name.Substring(Indexes[0, 0], Indexes[0, 1] - Indexes[0, 0] + 1);
                Right = Name.Substring(Indexes[0, 1] + 1, Indexes[1, 0] - Indexes[0, 1] - 1);
                Second = Name.Substring(Indexes[1, 0], Indexes[1, 1] - Indexes[1, 0] + 1);
                Final = Name.Substring(Indexes[1, 1] + 1);
            }

        }

        public void Index(string wordName)
        {
            if (Indexes[0, 0] == 0 && Indexes[0, 1] == 0)
            {
                if (Name == null)
                {
                    return;
                }

                int start = Name.ToLower().IndexOf(wordName);
                int finish;

                if (start == -1)
                {
                    //MessageBox.Show($"Пример слова {wordName} не удалось проиндексировать.", "Индексация примера", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }

                char[] validChars = new char[] { ',', ';', '!', '?', ' ', '.'};
                int least = Name.Length - 1;

                foreach (var c in validChars)
                {
                    finish = Name.ToLower().IndexOf(c, start);

                    if (finish != -1)
                    {
                        if (finish < least)
                            least = finish;
                    }
                }

                least -= 1;
                Indexes = new int[,] { { start, least }, { 0, 0 } };
            }
        }

        public bool ShouldSerializeName()
        {
            if (this == null || Name == null)
                return false;

            return true;
        }

        public bool ShouldSerializeIndexes()
        {
            if (this == null || Name == null)
                return false;

            return true;
        }

        public bool ShouldSerializeLeft()
        {
            return false;
        }

        public bool ShouldSerializeMain()
        {
            return false;
        }

        public bool ShouldSerializeRight()
        {
            return false;
        }

        public bool ShouldSerializeSecond()
        {
            return false;
        }

        public bool ShouldSerializeFinal()
        {
            return false;
        }
    }
}
