using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cegesauto
{
    class Data
    {
        public int nap;
        public string ido_egybe;
        public int ora;
        public int perc;
        public string rendszam;
        public string id;
        public int km;
        public int ki_be;
        public bool KI_BE;

        public Data(string s)
        {
            nap = int.Parse(s.Split(' ')[0]);
            ido_egybe = s.Split(' ')[1];
            ora = int.Parse(ido_egybe.Split(':')[0]);
            perc = int.Parse(ido_egybe.Split(':')[1]);
            rendszam = s.Split(' ')[2];
            id = s.Split(' ')[3];
            km = int.Parse(s.Split(' ')[4]);
            ki_be = int.Parse(s.Split(' ')[5]);
            if (ki_be > 0)
            {
                KI_BE = true;
            }
            else
            {
                KI_BE = false;
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region 1.feladat
            Console.WriteLine("1. Feladat");
            List<Data> Adatok = new List<Data>();
            StreamReader sr = new StreamReader("autok.txt");

            while (!sr.EndOfStream)
            {
                Adatok.Add(new Data(sr.ReadLine()));
            }

            sr.Close();

            #endregion

            #region 2.feladat
            Console.WriteLine("2. Feladat");
            int utolsonap_cache = 0;
            int legnagyobbora_cache = 0;
            int legnagyobbperc_cache = 0;
            string rendszam_cache = string.Empty;
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (Adatok[i].KI_BE == false)
                {
                    if (utolsonap_cache < Adatok[i].nap)
                    {
                        utolsonap_cache = Adatok[i].nap;
                    }
                }

            }
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (Adatok[i].KI_BE == false)
                {
                    if (utolsonap_cache == Adatok[i].nap)
                    {
                        if (legnagyobbora_cache < Adatok[i].ora)
                        {
                            legnagyobbora_cache = Adatok[i].ora;
                        }

                    }
                }
            }
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (Adatok[i].KI_BE == false)
                {
                    if (utolsonap_cache == Adatok[i].nap)
                    {
                        if (legnagyobbora_cache == Adatok[i].ora)
                        {
                            if (legnagyobbperc_cache < Adatok[i].perc)
                            {
                                legnagyobbperc_cache = Adatok[i].perc;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (Adatok[i].KI_BE == false)
                {
                    {
                        if (utolsonap_cache == Adatok[i].nap)
                        {
                            if (legnagyobbora_cache == Adatok[i].ora)
                            {
                                if (legnagyobbperc_cache == Adatok[i].perc)
                                {
                                    rendszam_cache = Adatok[i].rendszam;
                                }
                            }
                        }
                    }
                }
            }


            Console.WriteLine($"{utolsonap_cache}. nap rendszám: {rendszam_cache}");
            #endregion

            #region 3.feladat
            Console.WriteLine("3. Feladat");
            Console.Write("Nap: ");
            int bevitt_nap = int.Parse(Console.ReadLine());
            Console.WriteLine($"Forgalom a(z) {bevitt_nap}. napon:");
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (bevitt_nap == Adatok[i].nap)
                {
                    if (Adatok[i].KI_BE == true)
                    {
                        Console.WriteLine($"{Adatok[i].ido_egybe} {Adatok[i].rendszam} {Adatok[i].id} Be");
                    }
                    else
                    {
                        Console.WriteLine($"{Adatok[i].ido_egybe} {Adatok[i].rendszam} {Adatok[i].id} Ki");
                    }

                }
            }

            #endregion

            #region 4.feladat
            Console.WriteLine("4. Feladat");
            List<string> rendszam = new List<string>();
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (!rendszam.Contains(Adatok[i].rendszam))
                {
                    rendszam.Add(Adatok[i].rendszam);
                }
            }

            int nemvisszahozott_auto = 0;


            for (int i = 0; i < rendszam.Count; i++)
            {
                bool kint_vagy_bent = false;
                for (int j = 0; j < Adatok.Count; j++)
                {
                    if (Adatok[j].rendszam == rendszam[i])
                    {
                        kint_vagy_bent = Adatok[j].KI_BE;
                    }
                }
                if (kint_vagy_bent == false)
                {
                    nemvisszahozott_auto++;
                }

            }
            Console.WriteLine($"A hónap végén {nemvisszahozott_auto} autót nem hoztak vissza.");

            #endregion

            #region 5.feladat
            //Készítsen statisztikát, és írja ki a képernyőre mind a 10 autó esetén az ebben a hónapban 
            //megtett távolságot kilométerben! A hónap végén még kint lévő autók esetén az utolsó
            //rögzített kilométerállással számoljon! A kiírásban az autók sorrendje tetszőleges lehet.
            Console.WriteLine("5.Feladat");



            for (int f = 0; f < rendszam.Count; f++) //ciklus az autókon át

            {
                List<int> kmora = new List<int>(); //lista létrehozása minden autónál
                for (int k = 0; k < Adatok.Count; k++)
                {
                    if (Adatok[k].rendszam == rendszam[f])
                    {
                        kmora.Add(Adatok[k].km);
                    }
                }
                /*int osszkm = 0;
                for (int j = 0; j < kmora.Count; j++) //ciklus az autók kmóráján át (10 db adat)
                {
                    if (j % 2 != 0)
                    {
                        osszkm += kmora[j] - kmora[j - 1];

                    }
                }
                Console.WriteLine("{0} {1}", rendszam[f], osszkm);
                */


                Console.WriteLine("{0} {1} km", rendszam[f], kmora[kmora.Count - 1] - kmora[0]); // azert jo ez mert autot ho vegen kivittek és nem hozták vissza akkor ugyanaz a km állás mint amikor kivittek legutobb
            }
            #endregion

            #region 6.feladat

            List<string> emberekaz = new();
            List<int> kmek = new List<int>();

            for (int i = 0; i<Adatok.Count; i++)
            {
                if (!emberekaz.Contains(Adatok[i].id))
                {
                    emberekaz.Add(Adatok[i].id);
                }
            }

            for (int i=0; i<emberekaz.Count; i++)
            {
                List<int> kmorak = new();
                for (int j = 0; j<Adatok.Count; j++)
                {
                    if (emberekaz[i] == Adatok[j].id)
                    {
                        kmorak.Add(Adatok[j].km);
                    }
                }
                List<int> megtettek = new();
                for (int j = 0; j < kmorak.Count; j++)
                {
                    if (j%2 != 0)
                    {
                        megtettek.Add(kmorak[j] - kmorak[j - 1]);
                    }
                }
                if (megtettek.Count > 0)
                {
                    kmek.Add(megtettek.Max());
                }
                else
                {
                    kmek.Add(0);
                }
            }

            Console.WriteLine($"Leghosszabb út: {kmek.Max()} km, személy: {emberekaz[kmek.IndexOf(kmek.Max())]}");

            #endregion

            #region 7.feladat
            Console.WriteLine("7. Feladat");
            #endregion
        }
    }
}
