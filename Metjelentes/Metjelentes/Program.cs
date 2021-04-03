using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace metjelentes_2020maj_0313
{
    class Data
    {
        public string telepules;
        public string ido_egyben; // ketté kell bontani
        public string szél_eros_egybe; // kette kell bontani
        public int homerseklet; // figyelni a 10nel kevesebb karaktereknel! (?)
        public string ora;
        public string perc;
        public string szelirany;
        public string erosseg;



        public Data(string s)
        {
            telepules = s.Split(' ')[0];
            ido_egyben = s.Split(' ')[1];
            ora = ido_egyben.Substring(0, 2);
            perc = ido_egyben.Substring(2, 2);
            szél_eros_egybe = s.Split(' ')[2];
            szelirany = szél_eros_egybe.Substring(0, 3);
            erosseg = szél_eros_egybe.Substring(3, 2);
            homerseklet = int.Parse(s.Split(' ')[3]);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region 1.Feladat
            List<Data> Adatok = new List<Data>();
            StreamReader sr = new StreamReader("tavirathu13.txt");

            while (!sr.EndOfStream)
            {
                Adatok.Add(new Data(sr.ReadLine()));
            }

            sr.Close();
            #endregion

            #region 2.feladat
            Console.WriteLine(" 2.Feladat");

            Console.Write("Adja meg egy település kódját! Település: ");
            string telepuleskod = Console.ReadLine();

            int ora_cache = 0;
            int perc_cache = 0;
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (telepuleskod == Adatok[i].telepules)
                {
                    if (int.Parse(Adatok[i].ora) >= ora_cache)
                    {
                        ora_cache = int.Parse(Adatok[i].ora);
                    }

                    if (ora_cache == int.Parse(Adatok[i].ora))
                    {
                        if (int.Parse(Adatok[i].perc) > perc_cache)
                        {
                            perc_cache = int.Parse(Adatok[i].perc);
                        }
                    }
                }
            }
            string maxido = string.Empty;

            if (ora_cache < 10)
            {
                maxido += "0";
            }
            maxido += ora_cache.ToString();
            maxido += ":";
            if (perc_cache < 10)
            {
                maxido += 0;

            }
            maxido += perc_cache.ToString();

            Console.WriteLine($"Az utolsó mérési adat a megadott településről {maxido}-kor érkezett.");

            #endregion

            #region 3.feladat
            Console.WriteLine(" 3.Feladat");
            int cache_maxhomerseklet = 0;
            string maxtelep = string.Empty;
            string mintelep = string.Empty;
            string maxidopont = string.Empty;
            string minidopont = string.Empty;

            int cache_minhomerseklet = Adatok[0].homerseklet;
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (Adatok[i].homerseklet > cache_maxhomerseklet)
                {
                    maxidopont = string.Empty;
                    cache_maxhomerseklet = Adatok[i].homerseklet;
                    maxidopont += $"{Adatok[i].ora}:{Adatok[i].perc}";
                    maxtelep = Adatok[i].telepules;
                }
                if (Adatok[i].homerseklet < cache_minhomerseklet)
                {
                    minidopont = string.Empty;
                    cache_minhomerseklet = Adatok[i].homerseklet;
                    minidopont += $"{Adatok[i].ora}:{Adatok[i].perc}";
                    mintelep = Adatok[i].telepules;
                }
            }
            Console.WriteLine($"A legalacsonyabb hőmérséklet: {mintelep} {minidopont} {cache_minhomerseklet} fok");
            Console.WriteLine($"A legmagasabb hőmérséklet: {maxtelep} {maxidopont} {cache_maxhomerseklet} fok");

            #endregion

            #region 4.feladat
            Console.WriteLine(" 4.Feladat");
            bool szelcsendigazhamis = false;
            for (int i = 0; i < Adatok.Count; i++)
            {
                if (Adatok[i].szél_eros_egybe == "00000")
                {
                    szelcsendigazhamis = true;
                    Console.WriteLine($"{Adatok[i].telepules} {Adatok[i].ora}:{Adatok[i].perc}");
                    // telepules idopont
                }

            }
            if (!szelcsendigazhamis)
            {
                Console.WriteLine("Nem Volt szélcsend a mérések idején");
            }
            #endregion
            #region 5.feladat
            Console.WriteLine(" 5.Feladat");

            List<string> varosok = new List<string>();

            for (int i = 0; i<Adatok.Count; i++)
            {
                if (!varosok.Contains(Adatok[i].telepules))
                {
                    varosok.Add(Adatok[i].telepules);
                }
            }

            for (int i = 0; i<varosok.Count; i++)
            {

                List<int> varoskozepho_adatok = new List<int>();
                Dictionary<int, bool> orak = new Dictionary<int, bool>
                {
                    { 1, false },
                    { 7, false },
                    { 13, false },
                    { 19, false }
                };

                int maxho = cache_minhomerseklet;
                int minho = cache_maxhomerseklet;

                for (int j = 0; j<Adatok.Count; j++)
                {
                    if(Adatok[j].telepules == varosok[i])
                    {
                        if(Adatok[j].homerseklet<minho)
                        {
                            minho = Adatok[j].homerseklet;
                        }
                        else if (Adatok[j].homerseklet>maxho)
                        {
                            maxho = Adatok[j].homerseklet;
                        }

                        if (int.Parse(Adatok[j].ora)==1)
                        {
                            varoskozepho_adatok.Add(Adatok[j].homerseklet);
                            orak[1] = true;
                            
                        }
                        else if (int.Parse(Adatok[j].ora)==7)
                        {
                            varoskozepho_adatok.Add(Adatok[j].homerseklet);
                            orak[7] = true;
                            
                        }
                        else if (int.Parse(Adatok[j].ora)==13)
                        {
                            varoskozepho_adatok.Add(Adatok[j].homerseklet);
                            orak[13] = true;
                            
                        }
                        else if (int.Parse(Adatok[j].ora)==19)
                        {
                            varoskozepho_adatok.Add(Adatok[j].homerseklet);
                            orak[19] = true;
                            
                        }
                        
                    }
                }

                if (!orak.Values.Contains(false))
                {
                    Console.WriteLine($"{varosok[i]} Középhőmérséklet: {Math.Round(varoskozepho_adatok.Average())}; Hőmérséklet-ingadozás: {maxho - minho}");
                }
                else
                {
                    Console.WriteLine($"{varosok[i]} Középhőmérséklet: NA; Hőmérséklet-ingadozás: {maxho - minho}");
                }
            }


            #endregion
            #region 6.feladat
            Console.WriteLine(" 6.Feladat");

            #endregion

        }
    }
}
