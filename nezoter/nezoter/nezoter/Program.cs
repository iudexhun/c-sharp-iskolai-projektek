using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace nezoter
{
    class Data
    {
        public int sor;
        public int szek;
        public bool foglalt;
        public int kategoria;

        public Data(char f, char k, int s, int sz)
        {
            sor = s;
            szek = sz;
            if (f == 'o')
            {
                foglalt = false;
            }
            else if (f =='x')
            {
                foglalt = true;
            }
            kategoria = int.Parse(k.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            #region 1.f

            List<Data> adatok = new List<Data>(); // létrehozzuk a listát (székek adatait tartalmazza)

            StreamReader sr1 = new StreamReader("foglaltsag.txt");
            StreamReader sr2 = new StreamReader("kategoria.txt");

            int sorok = 0;

            while (!sr1.EndOfStream)
            {
                string sor = sr1.ReadLine();
                sorok++;
                string sorkat = sr2.ReadLine();

                for (int i = 0; i<sor.Length; i++)
                {
                    adatok.Add(new Data(sor[i], sorkat[i], sorok, i+1));
                }

            }
            sr1.Close();
            sr2.Close();

            #endregion

            #region 2.f
            Console.WriteLine("2. feladat");
            Console.Write("Adja meg egy sor számát: ");
            int sorsz = int.Parse(Console.ReadLine());
            Console.Write("Adja meg egy szék számát: ");
            int szeksz = int.Parse(Console.ReadLine());

            for (int i = 0; i<adatok.Count; i++)
            {
                if (adatok[i].sor == sorsz && adatok[i].szek == szeksz)
                {
                    Console.Write("A keresett hely ");

                    if (adatok[i].foglalt)
                    {
                        Console.WriteLine("foglalt.");
                    }
                    else
                    {
                        Console.WriteLine("szabad.");
                    }
                }
            }

            #endregion

            #region 3.f
            Console.WriteLine("3. feladat");
            int eladott = 0;
            for  (int i = 0; i<adatok.Count; i++)
            {
                if (adatok[i].foglalt)
                {
                    eladott++;
                }
            }

            double szazalek = Math.Round(eladott / (double) adatok.Count * 100);

            Console.WriteLine($"Az előadásra eddig {eladott} jegyet adtak el, ez a nézőtér {szazalek}%-a.");

            #endregion

            #region 4.f

            Console.WriteLine("4. feladat");

            int[] katok = new int[5] { 0, 0, 0, 0, 0 }; //5db 0-t tárol

            for (int i = 0; i<adatok.Count; i++)
            {
                if (adatok[i].foglalt)
                {
                    katok[adatok[i].kategoria - 1]++;
                }
            }

            int max = 0;
            int maxhely = 0;

            for (int i = 0; i<katok.Length; i++)
            {
                if (katok[i] > max)
                {
                    max = katok[i];
                    maxhely = i + 1;
                }
            }

            Console.WriteLine($"A legtöbb jegyet a(z) {maxhely}. árkategóriában értékesítették.");

            #endregion

            #region 5.f

            Console.WriteLine("5. feladat");
            int bevetel = katok[0] * 5000 + katok[1] * 4000 + katok[2] * 3000 + katok[3] * 2000 + katok[4] * 1500;
            Console.WriteLine($"A bevétel a jelenlegi állás szerint {bevetel} Ft lenne.");

            #endregion

            #region 6.f

            int egyedulallo = 0;

            for (int i=0; i<adatok.Count; i++)
            {
                if (!adatok[i].foglalt)
                {
                    if (adatok[i].szek == 1)
                    {
                        if (adatok[i].sor == adatok[i + 1].sor && adatok[i + 1].szek == 2 && adatok[i + 1].foglalt)
                        {
                            egyedulallo++;
                        }
                    }
                    else if (adatok[i].szek == 20)
                    {
                        if (adatok[i].sor == adatok[i - 1].sor && adatok[i - 1].szek == 19 && adatok[i - 1].foglalt)
                        {
                            egyedulallo++;
                        }
                    }
                    else
                    {
                        if (adatok[i].sor == adatok[i + 1].sor && adatok[i].sor == adatok[i - 1].sor && adatok[i+1].foglalt && adatok[i-1].foglalt)
                        {
                            egyedulallo++;
                        }
                    }
                }
            }

            Console.WriteLine($"Jelenleg {egyedulallo} \"egyedülálló\" üres hely van a nézőtéren.");

            #endregion

            #region 7.f /1. megoldás ("smart way")
            /*
            sr1 = new StreamReader("foglaltsag.txt");
            sr2 = new StreamReader("kategoria.txt");
            StreamWriter sw = new StreamWriter("szabad.txt");

            while (!sr1.EndOfStream)
            {
                string s1 = sr1.ReadLine();
                string s2 = sr2.ReadLine();
                for (int i = 0; i<s1.Length; i++)
                {
                    if (s1[i] == 'x')
                    {
                        sw.Write(s1[i]);
                    }
                    else
                    {
                        sw.Write(s2[i]);
                    }
                }
                sw.WriteLine();
            }
            
            sr1.Close();
            sr2.Close();
            sw.Close();
            */
            #endregion
            #region 7.f /2. megoldás ("hard way")

            StreamWriter sw = new StreamWriter("szabad.txt");

            for (int i=0; i<adatok.Count; i++)
            {
                if (i == adatok.Count-1)
                {
                    if (adatok[i].foglalt)
                    {
                        sw.Write('x');
                    }
                    else
                    {
                        sw.Write(adatok[i].kategoria);
                    }
                }
                else if (adatok[i].sor<adatok[i+1].sor)
                {
                    if (adatok[i].foglalt)
                    {
                        sw.WriteLine("x");
                    }
                    else
                    {
                        sw.WriteLine(adatok[i].kategoria);
                    }
                }
                else
                {
                    if (adatok[i].foglalt)
                    {
                        sw.Write('x');
                    }
                    else
                    {
                        sw.Write(adatok[i].kategoria);
                    }
                }
            }

            sw.Close();
            #endregion


        }
    }
}
