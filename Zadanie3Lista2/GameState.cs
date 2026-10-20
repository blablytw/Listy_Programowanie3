using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3Lista2
{
    public class GameState
    {
        public int[,] Ships1 = new int[5, 5];
        public int[,] Ships2 = new int[5, 5];

        public bool[,] Shots1 = new bool[5, 5];
        public bool[,] Shots2 = new bool[5, 5];

        public int Turn = 0;
        public int Winner = 0;

        public event Action Updated;

        public int CountShips(int player)
        {
            int[,] ships = player == 1 ? Ships1 : Ships2;
            int count = 0;

            foreach (int ship in ships)
            {
                if (ship == 1)
                    count++;
            }

            return count;
        }

        public void PlaceShip(int player, int row, int col)
        {
            if (Turn != 0)
                return;

            int[,] ships = player == 1 ? Ships1 : Ships2;

            if (CountShips(player) >= 3)
                return;

            if (ships[row, col] == 1)
                return;

            ships[row, col] = 1;

            if (CountShips(1) == 3 && CountShips(2) == 3)
                Turn = 1;

            Updated?.Invoke();
        }

        public void Shoot(int player, int row, int col)
        {
            if (Turn != player || Winner != 0)
                return;

            bool[,] shots = player == 1 ? Shots1 : Shots2;
            int[,] enemyShips = player == 1 ? Ships2 : Ships1;

            if (shots[row, col])
                return;

            shots[row, col] = true;

            int hits = 0;

            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (enemyShips[r, c] == 1 && shots[r, c])
                    {
                        // Strzaly sa zapisane na planszy strzelajacego.
                        hits++;
                    }
                }
            }

            // Liczymy trafienia na planszy przeciwnika.
            hits = 0;

            bool[,] enemyShots = player == 1 ? Shots1 : Shots2;

            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (enemyShips[r, c] == 1 && enemyShots[r, c])
                        hits++;
                }
            }

            if (hits == 3)
                Winner = player;
            else
                Turn = player == 1 ? 2 : 1;

            Updated?.Invoke();
        }
    }
}
