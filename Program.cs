using System;
using System.Collections.Generic;

namespace AlkuainePeli
{
    class Program
    {
        static void Main(string[] args)
        {
            string tiedostopolku = "alkuaineet.txt";
            var alkuaineet = AlkuaineLataaja.LataaAlkuaineet(tiedostopolku);

            if (alkuaineet.Count > 0)
            {
                Console.WriteLine("Haluatko pelata (p) vai tarkastella tuloksia (t)?");
                string valinta = Console.ReadLine().ToLower();

                if (valinta == "p")
                {
                    Peli.Pelaa(alkuaineet);
                }
                else if (valinta == "t")
                {
                    TulosTallentaja.TarkasteleTuloksia();
                }
                else
                {
                    Console.WriteLine("Virheellinen valinta.");
                }
            }
        }
    }
}
