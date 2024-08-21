using System;
using System.Collections.Generic;
using System.IO;

namespace AlkuainePeli
{
    public static class AlkuaineLataaja
    {
        public static Dictionary<string, string> LataaAlkuaineet(string tiedostopolku)
        {
            var alkuaineet = new Dictionary<string, string>();

            // Käytetään suoritushakemistoa tiedoston etsimiseen
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, tiedostopolku);

            Console.WriteLine($"Kokeillaan ladata tiedostosta: {fullPath}");

            if (File.Exists(fullPath))
            {
                var rivit = File.ReadAllLines(fullPath);
                foreach (var rivi in rivit)
                {
                    var osat = rivi.Split(',');
                    if (osat.Length == 2)
                    {
                        alkuaineet[osat[0].Trim()] = osat[1].Trim();
                    }
                }
            }
            else
            {
                Console.WriteLine($"Virhe: Alkuaineet-tiedostoa '{fullPath}' ei löytynyt.");
            }
            return alkuaineet;
        }
    }
}
