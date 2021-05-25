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

        public bool ShouldSerializeName()
        {
            if (this == null || Name == null)
                return false;

            return true;
        }
    }
}
