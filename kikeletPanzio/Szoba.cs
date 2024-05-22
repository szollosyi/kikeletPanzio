using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kikeletPanzio
{
    internal class Szoba
    {
        public int Szobaszam { get; set; }
        public int Ferohely { get; set; }
        public int Ar { get; set; }
        public string UgyfelNev { get; set; }
        public string UgyfelEmail { get; set; }
        public DateTime UgyfelSzuletesiDatum { get; set; }
        public bool UgyfelVIP { get; set; }
        public int KedvezmenyesAr => UgyfelVIP ? (int)(Ar * 0.97) : Ar;

        public Szoba(int szobaszam, int ferohely, int ar, Ugyfel ugyfel)
        {
            Szobaszam = szobaszam;
            Ferohely = ferohely;
            Ar = ar;
            UgyfelNev = ugyfel.Nev;
            UgyfelEmail = ugyfel.Email;
            UgyfelSzuletesiDatum = ugyfel.SzuletesiDatum;
            UgyfelVIP = ugyfel.VIP;
        }

        public override string ToString()
        {
            return $"Szobaszám: {Szobaszam}\nFérőhely: {Ferohely}\nÁr: {KedvezmenyesAr}\nÜgyfél: {UgyfelNev}\nE-mail: {UgyfelEmail}\nSzületési dátum: {UgyfelSzuletesiDatum.ToShortDateString()}\nVIP: {UgyfelVIP}";
        }
    }
}