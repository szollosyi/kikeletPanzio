using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kikeletPanzio
{
    public class Ugyfel
    {
        public string Nev { get; set; }
        public DateTime SzuletesiDatum { get; set; }
        public string Email { get; set; }
        public bool VIP { get; set; }

        public Ugyfel(string nev, DateTime szuletesiDatum, string email, bool vip)
        {
            Nev = nev;
            SzuletesiDatum = szuletesiDatum;
            Email = email;
            VIP = vip;
        }

        public override string ToString()
        {
            return Nev;
        }
    }
}
