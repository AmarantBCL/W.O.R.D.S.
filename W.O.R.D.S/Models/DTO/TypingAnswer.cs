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
                Example example = mode.Word.GetExample();
                if (example.Main == "-")
                    Text = mode.Translation;     
                else
                    Text = example.Main.ToLower();

                if (example.Second != "")
                    SecondText = example.Second;
            }
            else
                Text = mode.Translation;

            MaxWidth = Text.Length * 20;
        }
    }
}
