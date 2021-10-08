using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W.O.R.D.S.Models.DTO
{
    class TypingAnswer
    {
        public string Text { get; set; }
        public string SecondText { get; set; } = "";
        public int MaxWidth { get; set; }

        public TypingAnswer(ICard mode)
        {
            if (mode is Context)
            {
                if (mode.Word.Example.Main == "-")
                    Text = mode.Translation;     
                else
                    Text = mode.Word.Example.Main.ToLower();

                if (mode.Word.Example.Second != "")
                    SecondText = mode.Word.Example.Second;
            }
            else
                Text = mode.Translation;

            MaxWidth = Text.Length * 20;
        }
    }
}
