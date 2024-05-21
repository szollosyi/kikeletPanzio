using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kikeletPanzio
{
    internal class Szoba
    {
        int szobaszam;
        int ferohely;
        int ar;

        public Szoba(int szobaszam, int ferohely, int ar)
        {
            this.Szobaszam = szobaszam;
            this.Ferohely = ferohely;
            this.Ar = ar;
        }

        public int Szobaszam { get => szobaszam; set => szobaszam = value; }
        public int Ferohely { get => ferohely; set => ferohely = value; }
        public int Ar { get => ar; set => ar = value; }

        public override string ToString()
        {
            return $"Szobaszám: {szobaszam}\nFérőhely: {ferohely}\nÁr: {ar}";
        }
    }
}
