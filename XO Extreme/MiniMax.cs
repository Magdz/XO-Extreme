using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO_Extreme
{
    class MiniMax
    {
        public static char Player { set; get; }
        public int move_x { get; private set; }
        public int move_y { get; private set; }


        public int Ai(char[,] game, char gamePlay, int depth)
        {
            char condition = checkGame(game);
            if(condition != '0') return score(condition, depth);
            List<int> scores = new List<int>();
            List<int> movesi = new List<int>();
            List<int> movesj = new List<int>();

            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (game[i,j] == ' ')
                    {
                        char[,] possible_game = copyTable(game);
                        possible_game[i,j] = gamePlay;
                        if (gamePlay == 'x') scores.Add(Ai(possible_game, 'o', depth + 1));
                        else scores.Add(Ai(possible_game, 'x', depth + 1));
                        movesi.Add(i);
                        movesj.Add(j);
                    }
                }
            }

            if (gamePlay == Player)
            {
                int maximum = scores.Max();
                int index = scores.IndexOf(maximum);
                move_x = movesi[index];
                move_y = movesj[index];
                return maximum;
            }
            else
            {
                int minimum = scores.Min();
                int index = scores.IndexOf(minimum);
                move_x = movesi[index];
                move_y = movesj[index];
                return minimum;
            }
        }

        public int score(char condition, int depth)
        {
            char Opp;
            if (Player == 'x') Opp = 'o';
            else Opp = 'x';
            if (condition == Player) return 10 - depth;
            else if (condition == Opp) return depth - 10;
            else return 0;
        }

        public char checkGame(char[,] table)
        {
            char gamePlay = 'x';
            if (rowCheck(table, gamePlay) || columnCheck(table, gamePlay) || dioCheck(table, gamePlay))
            {
                return gamePlay;
            }
            gamePlay = 'o';
            if (rowCheck(table, gamePlay) || columnCheck(table, gamePlay) || dioCheck(table, gamePlay))
            {
                return gamePlay;
            }
            int Spaces = 0;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (table[i,j] == ' ') Spaces++;
                }
            }
            if (Spaces == 0) return 'D';
            return '0';
        }

        public bool rowCheck(char[,] table, char gamePlay)
        {
            for (int i = 0; i < 3; i++)
            {
                if (table[i, 0] == gamePlay && table[i, 1] == gamePlay && table[i, 2] == gamePlay) return true;
            }
            return false;
        }

        public bool columnCheck(char[,] table, char gamePlay)
        {
            for (int i = 0; i < 3; i++)
            {
                if (table[0,i] == gamePlay && table[1,i] == gamePlay && table[2,i] == gamePlay) return true;
            }
            return false;
        }

        public bool dioCheck(char[,] table, char gamePlay)
        {
            if (table[0,0] == gamePlay && table[1,1] == gamePlay && table[2,2] == gamePlay) return true;
            if (table[0,2] == gamePlay && table[1,1] == gamePlay && table[2,0] == gamePlay) return true;
            return false;
        }

        public char[,] copyTable(char[,] table){
        char[,] newTable = new char[3,3];
        for(int i=0; i<3; ++i){
            for(int j=0; j<3; ++j){
                newTable[i,j] = table[i,j];
            }
        }
        return newTable;
    }

    }
}
