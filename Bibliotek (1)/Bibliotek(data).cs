using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek__1_
{
    internal class Böcker
    {
        internal static int count;

        public string Författare { get; set; }
        public string BokNamn {  get; set; }

        public Böcker(string författare, string bokNamn)
        {
             Författare = författare;
             BokNamn = bokNamn;
        }   

    }
    
    internal class LånadeBöcker
    {

        public string Förfatare { get; set; }
        public string BokNamn { get; set; }

        public LånadeBöcker(string författare, string bokNamn)
        {
            Förfatare = författare;
            BokNamn = bokNamn;

        }
    }
}
