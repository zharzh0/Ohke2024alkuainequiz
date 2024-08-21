using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace AlkuainePeli
{
    public static class TulosTallentaja
    {
        public static void TallennaTulos(int tulos)
        {
            string hakemisto = "tulos";
            if (!Directory.Exists(hakemisto))
            {
                Directory.CreateDirectory(hakemisto);
            }

            string tiedostopolku = Path.Combine(hakemisto, "tulokset.json");

            var tulosLista = new List<int>();

            if (File.Exists(tiedostopolku))
            {
                string aiempiSisalto = File.ReadAllText(tiedostopolku);
                tulosLista = JsonConvert.DeserializeObject<List<int>>(aiempiSisalto);
            }

            tulosLista.Add(tulos);
            string uusiSisalto = JsonConvert.SerializeObject(tulosLista);
            File.WriteAllText(tiedostopolku, uusiSisalto);
        }

        public static void TarkasteleTuloksia()
        {
            string hakemisto = "tulos";
            string tiedostopolku = Path.Combine(hakemisto, "tulokset.json");

            if (File.Exists(tiedostopolku))
            {
                string sisalto = File.ReadAllText(tiedostopolku);
                var kaikkiTulokset = JsonConvert.DeserializeObject<List<int>>(sisalto);

                if (kaikkiTulokset.Count > 0)
                {
                    double keskiarvo = kaikkiTulokset.Average();
                    Console.WriteLine($"Tulosten keskiarvo: {keskiarvo}");
                }
                else
                {
                    Console.WriteLine("Ei tuloksia saatavilla.");
                }
            }
            else
            {
                Console.WriteLine("Ei tuloksia saatavilla.");
            }
        }
    }
}
