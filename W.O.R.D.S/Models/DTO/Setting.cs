using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W.O.R.D.S.Models.DTO
{
    static class Setting
    {
        public static bool ShowOrder { get; set; } = true;
        public static bool ShowCategory { get; set; } = true;
        public static bool ShowLevel { get; set; } = true;
        public static bool ShowPartOfSpeech { get; set; } = true;
        public static bool ShowTranscription { get; set; } = true;
        public static bool ShowMeaning { get; set; } = true;
        public static bool ShowExamples { get; set; } = true;
        public static List<int> Exceptions { get; set; } = new List<int>();
        public static Vocabulary Vocabulary { get; set; } = Vocabulary.Read()[0];
        public static int WordsAmount { get; set; } = 5;
        public static Category Category { get; set; } = Category.Read()[0];
        public static string GameMode { get; set; } = "1";

    }
}
