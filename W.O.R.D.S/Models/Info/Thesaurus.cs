using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W.O.R.D.S.Models.Info
{
    public class Thesaurus
    {
        public string Name { get; set; }
        public string Pic { get; set; }

        public Thesaurus(string name, string pic)
        {
            Name = name;
            Pic = "/Icons/" + pic + ".png";
        }
    }
}
