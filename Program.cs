using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

class Program
{
    static Dictionary<string, string> LataaAlkuaineet(string tiedostopolku)
    {
        var alkuaineet = new Dictionary<string, string>();
        var rivit = File.ReadAllLines(tiedostopolku);

        foreach (var rivi in rivit)
        {
            var osat = rivi.Split(',');
            if (osat.Length == 2)
            {
                alkuaineet[osat[0].Trim()] = osat[1].Trim();
            }
        }

        return alkuaineet;
    }

    static void Pelaa(Dictionary<string, string> alkuaineet)
    {
        var oikeatVastaukset = 0;
        var kysytytAlkuaineet = new HashSet<string>();
        var random = new Random();
        var alkuaineidenNimet = new List<string>(alkuaineet.Keys);

        for (int i = 0; i < 5; i++)
        {
            string kysyttavaAlkuaine;

            // Etsitään uusi alkuaine, jota ei ole vielä kysytty
            do
            {
                kysyttavaAlkuaine = alkuaineidenNimet[random.Next(alkuaineidenNimet.Count)];
            } while (kysytytAlkuaineet.Contains(kysyttavaAlkuaine));

            kysytytAlkuaineet.Add(kysyttavaAlkuaine);

            Console.WriteLine($"Anna alkuaineen '{kysyttavaAlkuaine}' kemiallinen merkki: ");
            string vastaus = Console.ReadLine();

            if (alkuaineet[kysyttavaAlkuaine].Equals(vastaus, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Oikein!");
                oikeatVastaukset++;
            }
            else
            {
                Console.WriteLine($"Väärin! Oikea vastaus on: {alkuaineet[kysyttavaAlkuaine]}");
            }
        }

        Console.WriteLine($"Oikeita vastauksia: {oikeatVastaukset}/5");
        TallennaTulos(oikeatVastaukset);
    }

    static void TallennaTulos(int tulos)
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

    static void TarkasteleTuloksia()
    {
        string[] hakemistot = Directory.GetDirectories(Directory.GetCurrentDirectory());
        var kaikkiTulokset = new List<int>();

        foreach (var hakemisto in hakemistot)
        {
            string tiedostopolku = Path.Combine(hakemisto, "tulokset.json");

            if (File.Exists(tiedostopolku))
            {
                string sisalto = File.ReadAllText(tiedostopolku);
                var tulokset = JsonConvert.DeserializeObject<List<int>>(sisalto);
                kaikkiTulokset.AddRange(tulokset);
            }
        }

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

    static void Main(string[] args)
    {
        string tiedostopolku = "alkuaineet.txt";
        var alkuaineet = LataaAlkuaineet(tiedostopolku);

        Console.WriteLine("Haluatko pelata (p) vai tarkastella tuloksia (t)?");
        string valinta = Console.ReadLine();

        if (valinta == "p")
        {
            Pelaa(alkuaineet);
        }
        else if (valinta == "t")
        {
            TarkasteleTuloksia();
        }
        else
        {
            Console.WriteLine("Virheellinen valinta.");
        }
    }
}
