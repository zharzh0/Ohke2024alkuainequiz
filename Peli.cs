using System;
using System.Collections.Generic;

namespace AlkuainePeli
{
    public static class Peli
    {
        public static void Pelaa(Dictionary<string, string> alkuaineet)
        {
            var oikeatVastaukset = 0;
            var kysytytAlkuaineet = new HashSet<string>();
            var random = new Random();
            var alkuaineidenNimet = new List<string>(alkuaineet.Keys);

            for (int i = 0; i < 5; i++)
            {
                string kysyttavaAlkuaine;

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
            TulosTallentaja.TallennaTulos(oikeatVastaukset);
        }
    }
}
