using System;
using System.Collections.Generic;
using System.IO;

namespace cegesautok
{
    class Auto
    {
        public int nap;
        public int ora;
        public int perc;
        public string rendszam;
        public int azon;
        public int km;
        public bool kibe;
        public Auto(string s) 
        {
            nap = int.Parse(s.Split(' ')[0]);
            string ido = s.Split(' ')[1];
            ora = int.Parse(ido.Split(':')[0]);
            perc = int.Parse(ido.Split(':')[1]);
            rendszam = s.Split(' ')[2];
            azon = int.Parse(s.Split(' ')[3]);
            km = int.Parse(s.Split(' ')[4]);
            if(s.Split(' ')[5]=="0")
            {
                kibe = false;
            }
            else
            {
                kibe = true;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            #region 1.f
            StreamReader sr = new StreamReader("autok.txt");
            List <Auto> L= new List<Auto>();
            while (!sr.EndOfStream)
            {
                L.Add(new Auto(sr.ReadLine()));
            }
            sr.Close();
            #endregion
            #region 2.f

            

            #endregion
        }
    }
}
