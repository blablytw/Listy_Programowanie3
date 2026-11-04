using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3Lista2
{
    public class StanGry
    {
        public bool[][] Statki = { new bool[0], new bool[100], new bool[100] };
        public bool[][] Strzaly = { new bool[0], new bool[100], new bool[100] };
        public OknoGry?[] Okna = new OknoGry?[3];

        public int Tura = 0;
        public int Zwyciezca = 0;

        public int Policz(bool[] plansza)
        {
            int ile = 0;

            foreach (bool pole in plansza)
            {
                if (pole)
                    ile++;
            }

            return ile;
        }

        public void Postaw(int gracz, int numer)
        {
            if (Tura != 0 || Policz(Statki[gracz]) >= 3 || Statki[gracz][numer])
                return;

            Statki[gracz][numer] = true;

            if (Policz(Statki[1]) == 3 && Policz(Statki[2]) == 3)
                Tura = 1;

            Odswiez();
        }

        public void Strzel(int gracz, int numer)
        {
            if (Tura != gracz || Zwyciezca != 0 || Strzaly[gracz][numer])
                return;

            Strzaly[gracz][numer] = true;

            int trafienia = 0;

            for (int i = 0; i < 100; i++)
            {
                if (Statki[3 - gracz][i] && Strzaly[gracz][i])
                    trafienia++;
            }

            if (trafienia == 3)
                Zwyciezca = gracz;
            else
                Tura = 3 - gracz;

            Odswiez();
        }

        public void Odswiez()
        {
            Okna[1]?.Odswiez();
            Okna[2]?.Odswiez();
        }
    }
}
