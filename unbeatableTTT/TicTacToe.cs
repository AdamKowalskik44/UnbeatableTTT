using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unbeatableTTT
{
    class TicTacToe

        //TODO - Do napisania fukcja:
        // -MakeTwoOptions
        // -co jeśli gracz nie zaczyna
        // -obsłużenie remisu
    {
        char[,] mat = new char[3, 3];
        char whoWon;
        char activePlayer, alivePlayer;
        const char emptyField = '0';
        const char player1 = 'X', player2 = 'O';
        int x, y;
        int turnCounter = 1;

        public char[,] Get_mat()
        {
            return mat;
        }

        public TicTacToe()
        {
            for (int i = 0; i < 3; i++) //filling the mat with empty chars
            {
                for (int j = 0; j < 3; j++)
                {
                    mat[i, j] = emptyField;
                }
            }

            Random rnd = new Random();
            int k = rnd.Next();
            if (k % 2 == 0) //determining the alive player
            {
                alivePlayer = player1;
            }
            else
            {
                alivePlayer = player2;
            }

            k = rnd.Next(); 
            if (k % 2 == 0) //determining the active player
            {
                activePlayer = player1;
            }
            else
            {
                activePlayer = player2;
            }

            Display(mat);

            activePlayer = player1; //DELET THIS AFTER
            alivePlayer = player2;
        }

        public void Display(char[,] matCopy)
        {
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(mat[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool Check(char[,] matCopy)
        {
            for (int i = 0; i < 3; i++) //checking rows
            {
                if (matCopy[0, i] ==  matCopy[1, i] && matCopy[1, i] ==  matCopy[2, i] && matCopy[0, i] != emptyField)
                {
                    whoWon = matCopy[0, i];
                    return true;
                }
            }

            for (int i = 0; i < 3; i++) //checking columns
            {
                if (matCopy[i, 0] ==  matCopy[i, 1] && matCopy[i, 0] == matCopy[i, 2] && matCopy[i, 0] != emptyField)
                {
                    whoWon = matCopy[i, 0];
                    return true;                    
                }
            }

            //checking diagonals
            if ((matCopy[0, 0] ==  matCopy[1, 1] && matCopy[0, 0] == matCopy[2, 2] && matCopy[1, 1] != emptyField) || (matCopy[2, 0] == matCopy[1, 1] && matCopy[0, 2] == matCopy[2, 2] && matCopy[1, 1] != emptyField))
            {
                whoWon = mat[1, 1];
                return true;
            }
            Console.WriteLine("brak wygranej");
            return false;
        }

        public void TakeTurn()
        {
            Console.WriteLine("_____________________________________TURN_START_____________________");
            Console.WriteLine("Current turn is: " + turnCounter);
            Display(mat);
            if (activePlayer == alivePlayer)
            {
                do
                {
                    Console.WriteLine("Player " + activePlayer + ": ");
                    Console.Write("choose coord x: ");
                    x = Int32.Parse(Console.ReadLine());
                    Console.Write("choose coord y: ");
                    y = Int32.Parse(Console.ReadLine());

                    if (mat[x, y] != emptyField)
                    {
                        Console.WriteLine("Wrong choice! Field " + x + ", " + y + " already occupied!");
                    }
                } while (mat[x, y] != emptyField);
                    
            }
            else
            {
                Play();
            }

            mat[x, y] = activePlayer; //finalizing player / computer choice, IMPORTANT

            if (activePlayer == player1) //changing active player
            {
                activePlayer = player2;
            }
            else
            {
                activePlayer = player1;
            }

            turnCounter++;
            Display(mat);
        }

        public void WhoWon()
        {
            Display(mat);
            Console.WriteLine();
            Console.WriteLine("The winer is: Player " + whoWon);
        }

        public int Play()
        {
            if (turnCounter == 1) //if turn == 0, then computer randomizes to play at one of corners
            {
                MarkRandomizedCorner();
            }

            else if (turnCounter == 3) //turn 3
            {
                //if other player placed his mark not in corner or middle, then place it in the middle
                if (mat[1, 0] == alivePlayer || mat[0, 1] == alivePlayer || mat[2, 1] == alivePlayer || mat[1, 2] == alivePlayer)
                {
                    x = 1;
                    y = 1;
                }

                else if (mat[1, 1] == alivePlayer) //if he placed his mark in the middle, then place mark in opposite corner to the one preivously placed
                {
                    FindOppositeCorner(activePlayer);
                }

                else //if he placed in one of corners, place mark in corner adjacent to corner of own mark
                {
                    FindAdjacentCorner(activePlayer);
                }
            }
            else if (turnCounter == 5 || turnCounter == 7)
            {
                if (LookForWin(mat, activePlayer) == true) //look for a win
                {
                    return 0;
                }
                else if (LookForWin(mat, alivePlayer) == true) //lok for a win from other player, if found, then mark it as own
                {
                    return 0;
                }
                else if (MakeTwoOptions(mat, activePlayer) == true) //look for a place to leave a mark, that in future turn may have two possibilities to win
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error! scenario not forseen. Fix your code :)");
                }                    
            }
            return 0;
        }

        public void MarkRandomizedCorner()
        {
            Random rnd = new Random();
            int k = rnd.Next(), l = rnd.Next();
            if (k % 2 == 0)
            {
                x = 0;
            }
            else
            {
                x = 2;
            }
            if (l % 2 == 0)
            {
                y = 0;
            }
            else
            {
                y = 2;
            }
        }

        public void FindOppositeCorner(char mark)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (mat[i, j] == mark)
                    {
                        if (i == 0)
                        {
                            x = 2;
                        }
                        else if (i == 2)
                        {
                            x = 0;
                        }

                        if (j == 0)
                        {
                            y = 2;
                        }
                        else if (j == 2)
                        {
                            y = 0;
                        }
                    }
                }
            }
        }

        public void FindAdjacentCorner(char mark)
        {
            int[,] coords = new int[2, 2];
            bool firstFound = false, secondFound = false;
            FindOppositeCorner(activePlayer);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (mat[i, j] == emptyField && (i == 0 || i == 2) && (j == 0 || j == 2))
                    {
                        if(i == x && j == y)
                        {
                            continue;
                        }
                        else
                        {
                            if (firstFound == false)
                            {
                                coords[0, 0] = i;
                                coords[0, 1] = j;
                                firstFound = true;
                            }
                            else
                            {
                                coords[1, 0] = i;
                                coords[1, 1] = j;
                                secondFound = true;
                            }
                        }
                    }
                }
            }

            if (secondFound == true)
            {
                Random rnd = new Random();
                int k = rnd.Next();
                if (k % 2 == 0)
                {
                    x = coords[0, 0];
                    y = coords[0, 1];
                }
                else
                {
                    x = coords[1, 0];
                    y = coords[1, 1];
                }
            }
            else
            {
                x = coords[0, 0];
                y = coords[0, 1];
            }
        }

        public bool LookForWin(char[,] matCopy, char forWho)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matCopy[i, j] == emptyField)
                    {
                        matCopy[i, j] = forWho;
                        if (Check(matCopy) == true)
                        {
                            x = i;
                            y = j;
                            return true;
                        }
                        else
                        {
                            matCopy[i, j] = emptyField;
                        }
                    }
                }
            }
            return false;
        }

        public bool MakeTwoOptions(char[,] matCopy, char forWho)
        {
            return false;
        }
    }
}
