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
            { "Допущение", "admit" }, // + concede, confess (Признание?)
            { "Извинения", "apology" }, // много чёрных тонов, слабо распознаётся изображение
            { "Появление", "appear" },
            { "Поимка преступников", "arrest" },
            { "Прибытие в место", "arrival" },
            { "Привлекательность", "attract" },
            { "Красота", "beauty" },
            { "Счёт", "bill" },
            { "Составная часть", "blocks" },
            { "Смелость", "brave" },
            { "Поломка", "break" },
            { "Спокойствие", "calm" },
            { "Забота и уход", "care" }, // иконка дублирует категорию
            { "Хватка", "catch" },
            { "Изменение", "change" },
            { "Выбор", "choose" },
            { "Чистота", "clean" },
            { "Ясность", "clear" },
            { "Подъём", "climb" },
            { "Холод", "cold" },
            { "Дорожно-транспортное происшествие", "crash" },
            // { "Обман", "deceive" }, // = con
            { "Сложность", "difficult" },
            // { "Неодобрение", "disapproval" }, // = condemn
            { "Объяснение", "explain" },
            { "Падение", "fall" },
            { "Драка и бой", "fight" },
            { "Огонь", "fire" },
            { "Радость и удовольствие", "happy" },
            { "Удар", "hit" }, // плохо видно на белом фоне
            { "Держание", "hold" }, // плоховато видно на светлом фоне
            { "Тепло", "hot" },
            { "Повреждения", "hurt" }, // не самая удачная иконка
            // { "Воображение", "imagine" }, // = conceive
            { "Увеличение", "increase" },
            { "Ум", "intelligent" },
            { "Знания", "know" },
            { "Смех", "laugh" },
            { "Изучение", "learn" },
            { "Смешивание", "mix" },
            { "Ловля животных", "net" }, // тусклая иконка
            { "Подчинение", "obey" },
            { "Обещание", "promise" },
            { "Воспоминание", "remember" },
            { "Поиск и нахождение", "search" },
            { "Свет", "shine" },
            { "Крик", "shout" },
            { "Начало", "start" },
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
