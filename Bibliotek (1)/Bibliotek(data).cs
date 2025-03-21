using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek__1_
{
    internal class Böcker
    {
        

        public string Författare { get; set; }
        public string BokNamn {  get; set; }
        public bool ÄrLånad { get; set; }
        public Böcker(string bokNamn, string författare, bool ärLånad)
        {
             Författare = författare;
             BokNamn = bokNamn;
             ÄrLånad = ärLånad;
        }   

    }
    
    internal class LånadeBöcker
    {

        public string Författare { get; set; }
        public string BokNamn { get; set; }

        

        public LånadeBöcker(string bokNamn, string författare)
        {
            Författare = författare;
            BokNamn = bokNamn;
            

        }
    }
}
