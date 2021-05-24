using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W.O.R.D.S.Models.DTO
{
    static class Setting
    {
        public static bool Order { get; set; } = true;
        public static bool Category { get; set; } = true;
        public static bool Level { get; set; } = true;
        public static bool PartOfSpeech { get; set; } = true;
        public static bool Transcription { get; set; } = true;
        public static bool Meaning { get; set; } = true;
        public static bool Examples { get; set; } = true;
        public static List<int> Exceptions { get; set; } = new List<int>();

    }
}
