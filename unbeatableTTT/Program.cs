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
            TicTacToe TicTacToe = new TicTacToe();
            TicTacToe.TakeTurn();
            Console.ReadKey();
        }
    }
}
