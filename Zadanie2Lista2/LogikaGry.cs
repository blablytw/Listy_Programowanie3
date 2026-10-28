using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie2Lista2
{
    public class LogikaGry
    {
        private static readonly int[][] Linie =
        {
            new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, new[] { 6, 7, 8 },
            new[] { 0, 3, 6 }, new[] { 1, 4, 7 }, new[] { 2, 5, 8 },
            new[] { 0, 4, 8 }, new[] { 2, 4, 6 }
        };

        private readonly string[] plansza = new string[9];

        public string Gracz { get; private set; } = "X";
        public bool KoniecGry { get; private set; }
        public string Status { get; private set; } = "Ruch gracza: X";

        public LogikaGry()
        {
            Reset();
        }

        public string this[int indeks] => plansza[indeks];

        public bool Ruch(int indeks)
        {
            if (KoniecGry || plansza[indeks] != "")
                return false;

            plansza[indeks] = Gracz;

            if (CzyWygrana())
            {
                Status = "Wygrywa gracz " + Gracz + "!";
                KoniecGry = true;
                return true;
            }

            if (CzyRemis())
            {
                Status = "Remis!";
                KoniecGry = true;
                return true;
            }

            Gracz = Gracz == "X" ? "O" : "X";
            Status = "Ruch gracza: " + Gracz;
            return true;
        }

        public void Reset()
        {
            for (int i = 0; i < plansza.Length; i++)
                plansza[i] = "";

            Gracz = "X";
            KoniecGry = false;
            Status = "Ruch gracza: X";
        }

        private bool CzyWygrana()
        {
            foreach (int[] l in Linie)
            {
                if (plansza[l[0]] == Gracz &&
                    plansza[l[1]] == Gracz &&
                    plansza[l[2]] == Gracz)
                    return true;
            }

            return false;
        }

        private bool CzyRemis()
        {
            foreach (string pole in plansza)
            {
                if (pole == "")
                    return false;
            }

            return true;
        }
    }
}
