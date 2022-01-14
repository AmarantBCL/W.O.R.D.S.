using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace W.O.R.D.S.Models.Info
{
    public static class Thesaurus
    {
        private const string PATTERN = @"\[(.+)\]";

        public static Dictionary<string, string> Article = new Dictionary<string, string>
        {
            { "Обвинение", "accuse" },
            { "Допущение", "admit" },
            { "Извинения", "apology" },
            { "Поимка преступников", "arrest" },
            { "Прибытие в место", "arrival" },
            { "Привлекательность", "attract" },
            { "Красота", "beauty" },
            { "Счёт", "bill" },
            { "Составная часть", "blocks" },
            { "Смелость", "brave" },
            { "Поломка", "break" },
            { "Спокойствие", "calm" },
            { "Забота и уход", "care" },
            { "Хватка", "catch" },
            { "Изменение", "change" },
            { "Холод", "cold" },
            { "Дорожно-транспортное происшествие", "crash" },
            { "Огонь", "fire" },
            { "Радость и удовольствие", "happy" },
            { "Удар", "hit" },
            { "Держание", "hold" },
            { "Тепло", "hot" },
            { "Повреждения", "hurt" },
            { "Увеличение", "increase" },
            { "Знания", "know" },
            { "Смех", "laugh" },
            { "Изучение", "learn" },
            { "Смешивание", "mix" },
            { "Ловля животных", "net" },
            { "Воспоминание", "remember" },
            { "Поиск и нахождение", "search" },
            { "Свет", "shine" },
            { "Крик", "shout" },
            { "Утверждение", "state" },
            { "Предположение", "suppose" },
            { "Общение", "talk" },
            { "Передвижение с кем-либо", "take" },
            { "Кидание и бросание", "throw" },
            { "Понимание", "understand" },
            { "Ширина", "width" },
            { "Моргание", "wink" },
        };

        public static string GetTag(string note)
        {
            Regex regex = new Regex(PATTERN);
            MatchCollection matches = regex.Matches(note);

            if (matches != null && matches.Count > 0)
            {
                foreach (Match match in matches)
                    return match.Groups[1].Value;
            }

            return "";
        }
    }
}
