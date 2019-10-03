using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unbeatableTTT
{
    class TicTacToe
    {
        #region class properties
        char[,] board = new char[3, 3];
        char whoWon;
        char activePlayer, alivePlayer;
        const char emptyField = '_';
        const char player1 = 'X', player2 = 'O';
        int x, y;
        int turnCounter = 1;
        #endregion

        public TicTacToe()
        {
            for (int i = 0; i < 3; i++) //filling the mat with empty chars
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = emptyField;
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

            Display(board);
        }

        /// <summary>
        /// Displays a values of passed array
        /// </summary>
        public void Display(char[,] board)
        {
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Checks if there is a win in passed array. Returns true if yes and changes value whoWon
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool Check(char[,] board)
        {
            for (int i = 0; i < 3; i++) //checking rows
            {
                if (board[0, i] ==  board[1, i] && board[1, i] ==  board[2, i] && board[0, i] != emptyField)
                {
                    whoWon = board[0, i];
                    return true;
                }
            }

            for (int i = 0; i < 3; i++) //checking columns
            {
                if (board[i, 0] ==  board[i, 1] && board[i, 0] == board[i, 2] && board[i, 0] != emptyField)
                {
                    whoWon = board[i, 0];
                    return true;                    
                }
            }

            //checking diagonals
            if ((board[0, 0] ==  board[1, 1] && board[0, 0] == board[2, 2] && board[1, 1] != emptyField) || (board[2, 0] == board[1, 1] && board[2, 0] == board[0, 2] && board[1, 1] != emptyField))
            {
                whoWon = board[1, 1];
                return true;
            }
            return false;
        }

        /// <summary>
        /// Main method of a program. Calls itself if turncounter < 10
        /// </summary>
        public void TakeTurn()
        {
            Console.WriteLine("_____________________________________TURN_START_____________________");
            Console.WriteLine("Current turn is: " + turnCounter);
            Display(board);
            if (activePlayer == alivePlayer)
            {
                do
                {
                    Console.WriteLine("Player " + activePlayer + ": ");
                    Console.Write("choose coord x: ");
                    x = Int32.Parse(Console.ReadLine());
                    Console.Write("choose coord y: ");
                    y = Int32.Parse(Console.ReadLine());

                    if (board[x, y] != emptyField)
                    {
                        Console.WriteLine("Wrong choice! Field " + x + ", " + y + " already occupied!");
                    }
                } while (board[x, y] != emptyField);
                    
            }
            else
            {
                Play();
            }

            if (board[x, y] != emptyField)
            {
                Console.WriteLine(board[x, y] != emptyField);
                Console.WriteLine("mat [" + x + ", " + y + "] = " + board[x, y]);
            }
            board[x, y] = activePlayer; //finalizing player / computer choice, IMPORTANT

            if (activePlayer == player1) //changing active player
            {
                activePlayer = player2;
            }
            else
            {
                activePlayer = player1;
            }

            turnCounter++;
            Display(board);

            Console.WriteLine("Check(mat) == " + Check(board));

            if (Check(board) == false)
            {
                if (turnCounter < 10)
                {
                    TakeTurn();                    
                }
                else
                {
                    Console.WriteLine("It's a draw!");
                }                
            }
            else
            {
                WhoWon();
            }
        }

        /// <summary>
        /// Displays who the winner is
        /// </summary>
        public void WhoWon()
        {
            Display(board);
            Console.WriteLine();
            Console.WriteLine("The winer is: Player " + whoWon);
        }

        /// <summary>
        /// Main method of a computer player
        /// </summary>
        /// <returns></returns>
        public int Play()
        {
            if (turnCounter == 1) //if turn == 0, then computer randomizes to play at one of corners
            {
                MarkRandomizedCorner();
            }
            else if (turnCounter == 2)
            {
                if (board[1, 1] == emptyField)
                {
                    x = 1;
                    y = 1;
                }
                else
                {
                    MarkRandomizedCorner();
                }
            }
            else if (turnCounter == 3) //turn 3
            {
                //if other player placed his mark not in corner or middle, then place it in the middle
                if (board[1, 0] == alivePlayer || board[0, 1] == alivePlayer || board[2, 1] == alivePlayer || board[1, 2] == alivePlayer)
                {
                    x = 1;
                    y = 1;
                }

                else if (board[1, 1] == alivePlayer) //if he placed his mark in the middle, then place mark in opposite corner to the one preivously placed
                {
                    FindOppositeCorner(activePlayer);
                }

                else //if he placed in one of corners, place mark in corner adjacent to corner of own mark
                {
                    FindAdjacentCorner(activePlayer);
                }
            }
            else
            {
                if (LookForWin(board, activePlayer, out x, out y) == true) //look for a win
                {
                    return 0;
                }
                else if (LookForWin(board, alivePlayer, out x, out y) == true) //lok for a win from other player, if found, then mark it as own
                {
                    return 0;
                }
                else if (MakeTwoOptions(board, activePlayer, out x, out y) == true) //look for a place to leave a mark, that in future turn may have two possibilities to win
                {
                    return 0;
                }
                else
                {
                    PlaceOnLastFreeSpot();
                }                    
            }
            return 0;
        }

        /// <summary>
        /// Marks random corner in a array. Should only be used on an array with empty corners
        /// </summary>
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

        /// <summary>
        /// marks the first free spot. Intended to be used on array with one empty spot.
        /// </summary>
        public void PlaceOnLastFreeSpot()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == emptyField)
                    {
                        x = i;
                        y = j;
                    }
                }
            }
        }

        /// <summary>
        /// Marks opposite corner to a already taken one (of the player given by variable mark). Should be used only on array with one corner taken
        /// </summary>
        /// <param name="mark"></param>
        public void FindOppositeCorner(char mark)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == mark)
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

        /// <summary>
        /// Marks corner adjacent to one already taken by player stated by mark variable. Randomizes one if there is to avalible. Should only be used on a array with one corner taken (by that player)
        /// </summary>
        /// <param name="mark"></param>
        public void FindAdjacentCorner(char mark)
        {
            int[,] coords = new int[2, 2];
            bool firstFound = false, secondFound = false;
            FindOppositeCorner(activePlayer);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == emptyField && (i == 0 || i == 2) && (j == 0 || j == 2))
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

        /// <summary>
        /// searches for a win situation in next move for a given player in given array. Returns true if found and outs x and y. Returns false if not found and outs current x and y
        /// </summary>
        /// <returns></returns>
        public bool LookForWin(char[,] mat, char forWho, out int x, out int y)
        {
            char[,] matCopy = new char[3, 3];
            Array.Copy(mat, 0, matCopy, 0, 9);
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

                            Console.WriteLine("Method LookForWin found match at: " + x + ", " + y);

                            return true;                           
                        }
                        else
                        {
                            matCopy[i, j] = emptyField;
                        }
                    }
                }
            }
            x = this.x;
            y = this.y;
            return false;
        }

        /// <summary>
        /// Searches for a move that will have to options to win in move after (so if one is blocked, the second remains) Returns true if found and outs x and y. Returns false if not found and outs current x and y
        /// </summary>
        /// <returns></returns>
        public bool MakeTwoOptions(char[,] mat, char forWho, out int x, out int y)
        {
            Console.WriteLine("looking for two options");
            char[,] matCopy = new char[3,3];
            Array.Copy(mat, 0, matCopy, 0, 9);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matCopy[i, j] == emptyField)
                    {
                        matCopy[i, j] = forWho;

                        if (LookForWin(matCopy, forWho, out x, out y) == true)
                        {
                            matCopy[x, y] = (forWho == activePlayer) ? alivePlayer : activePlayer;
                            if (LookForWin(matCopy, forWho, out _, out _) == true)
                            {
                                x = i;
                                y = j;
                                Console.WriteLine("zwracam x = " + x);
                                Console.WriteLine("zwracam y = " + y);
                                return true;
                            }
                            matCopy[x, y] = emptyField;
                        }
                        matCopy[i, j] = emptyField;
                    }
                }
            }
            x = this.x;
            y = this.y;
            return false;
        }
    }
}
