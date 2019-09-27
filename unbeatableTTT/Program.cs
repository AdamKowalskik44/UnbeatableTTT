using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unbeatableTTT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            TicTacToe TicTacToe = new TicTacToe();

            do
            {
                TicTacToe.TakeTurn();
            } while (TicTacToe.Check(TicTacToe.Get_mat()) == false);

            TicTacToe.WhoWon();

            Console.ReadKey();
        }
    }
}
