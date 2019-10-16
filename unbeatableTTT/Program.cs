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
            Console.WriteLine("hello there");
            TicTacToe ticTacToe = new TicTacToe();
            ticTacToe.TakeTurn();
            Console.ReadKey();
        }
    }
}
