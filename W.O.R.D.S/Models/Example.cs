using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W.O.R.D.S.Models
{
    public class Example
    {
        public string Name { get; set; }
        public int[] Indexes { get; set; } = { 0, 0 };
        public string Left { get; set; }
        public string Main { get; set; }
        public string Right { get; set; }

        public Example(string name)
        {
            Name = name;
        }

        public void FormatExample()
        {
            Left = Name.Substring(0, Indexes[0]);
            Main = Name.Substring(Indexes[0], Indexes[1] - Indexes[0] + 1);
            Right = Name.Substring(Indexes[1] + 1);
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
    }
}
