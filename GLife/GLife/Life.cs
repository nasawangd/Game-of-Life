//Date----------------------Developer-----------description
//02/18/2019---------------Derek Nasawang-------Created the class Life with variables int NumRows, int NumCols, char Live, char Dead.
//02/18/2019---------------Derek Nasawang-------Created a two 2 dimensional arrays called mainBoard and prepBoard, each with the NumRow and NumCol variables as its dimension.
//02/18/2019---------------Derek Nasawang-------Created the method PlayTheGame that will get the number of generations from the user and process and print the gameboard as many times as the user states.
//02/18/2019---------------Derek Nasawang-------Created the method Process gameboard that iterates through game board and determine what the next gen will be.
//02/18/2019---------------Derek Nasawang-------Created the swapgameboard method to swap the gameboards to prepare to play again.
//02/18/2019---------------Derek Nasawang-------Created the gamerules dead or alive with these rules in mind
//                                              Any live cell with fewer than two live neighbors dies, as if by underpopulation.
//                                              Any live cell with more than three live neighbors dies, as if by overpopulation. 
//02/18/2019---------------Derek Nasawang-------The get neighbor count method was created to be the primary game logic, checking to see if any of it pieces are alive and acting accordingly.
//02/18/2019---------------Derek Nasawang-------Created the startup config pattern to create a test case. 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLife
{
    class Life
    {
        const int NumRows = 60;
        const int NumCols = 80;
        const char Live = '@';
        const char Dead = '-';

        char[,] mainBoard = new char[NumRows, NumCols];
        char[,] prepBoard = new char[NumRows, NumCols];

        static void Main(string[] args)
        {
            Life game = new Life();
        }

        public Life()
        {
            Console.Clear();
            InitializeGameBoard(mainBoard);
            InitializeGameBoard(prepBoard);

            //arbitrarily put in some life
            StartupConfigPattern01(10, 5);
            StartupConfigPattern01(20, 15);
            StartupConfigPattern01(30, 20);

            DisplayUI();
            PlayTheGame();
        }

        public void DisplayUI()
        {
            Console.WriteLine(
@" Welcome to the game of life, please enter in the number of generations you would like to have the game of life play.
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
");
        }

        public void PlayTheGame()
        {
            //Get the number of generations from the user
            Console.Write("Generations to display: ");
            int genNum = int.Parse(Console.ReadLine());
            for (int i = 0; i < genNum; i++)
            {
                //This is the execution of a single generation of the simulation
                ProcessGameBoard();

                PrintGameBoard(mainBoard);
                Console.Clear();
            }
            PrintGameBoard(mainBoard);
        }

        //Iterate completely through the game board and determine
        //what the next generation will be for each cell
        private void ProcessGameBoard()
        {
            int neighbors = 0;
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    //theBoard[row, col] = Dead; -- recycled code
                    //Will this cell be dead or alive next time?
                    //C=0 means you're on the edge, and R=0 means you're on the top edge
                    //C.Length - 1 is the right most edge, and R.Length - 1 is the bottom edge

                    neighbors = GetNeighborCount(row, col);
                    ApplyDeadOrAliveRules(neighbors, row, col);

                }
            }

            //swap the two boards
            SwapGameBoards();
        }

        //Swap the two boards to get ready to start over again
        private void SwapGameBoards()
        {
            char[,] tmp = mainBoard;
            mainBoard = prepBoard;
            prepBoard = tmp;
        }

        private void ApplyDeadOrAliveRules(int neighbors, int r, int c)
        {
            //Any live cell with fewer than two live neighbors dies, as if by underpopulation.
            //Any live cell with more than three live neighbors dies, as if by overpopulation.
            if(neighbors < 2 || neighbors > 3)
            {
                prepBoard[r, c] = Dead;

            } else if(neighbors == 3) {
                prepBoard[r, c] = Live;

            } else
            {
                prepBoard[r, c] = mainBoard[r, c];

            }

            //Any live cell with two or three live neighbors lives on to the next generation.
            

            //Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
            //3N ==> LIVE
        }

        private int GetNeighborCount(int r, int c)
        {
            int neighborCounter = 0;
            if (r == 0 && c == 0)
            {
                //Top left corner

                //my row
                if (mainBoard[r, c + 1] == Live) neighborCounter++;
                //row below
                if (mainBoard[r + 1, c] == Live) neighborCounter++;
                if (mainBoard[r + 1, c + 1] == Live) neighborCounter++;

            } else if (r == 0 && c == NumCols - 1) {
                //my row
                if (mainBoard[r, c - 1] == Live) neighborCounter++;

                //row below
                if (mainBoard[r + 1, c - 1] == Live) neighborCounter++;
                if (mainBoard[r + 1, c] == Live) neighborCounter++;

            } else if (r == NumRows - 1 && c == NumCols - 1)
            {                
                //top right corner
                //row above
                if (mainBoard[r - 1, c - 1] == Live) neighborCounter++;
                if (mainBoard[r - 1, c] == Live) neighborCounter++;

                //my row
                if (mainBoard[r, c - 1] == Live) neighborCounter++;

            } else if (r == NumRows - 1 && c == 0)
            {
                //row above
                if (mainBoard[r - 1, c] == Live) neighborCounter++;
                if (mainBoard[r - 1, c + 1] == Live) neighborCounter++;

                //my row
                if (mainBoard[r, c + 1] == Live) neighborCounter++;

            } else if (r == 0)
            {                
                //my row
                if (mainBoard[r, c - 1] == Live) neighborCounter++;
                if (mainBoard[r, c + 1] == Live) neighborCounter++;

                //row below
                if (mainBoard[r + 1, c - 1] == Live) neighborCounter++;
                if (mainBoard[r + 1, c] == Live) neighborCounter++;
                if (mainBoard[r + 1, c + 1] == Live) neighborCounter++;

            } else if (c == 0)
            {
                //row above
                if (mainBoard[r - 1, c] == Live) neighborCounter++;
                if (mainBoard[r - 1, c + 1] == Live) neighborCounter++;

                //my row
                if (mainBoard[r, c + 1] == Live) neighborCounter++;

                //row below
                if (mainBoard[r + 1, c] == Live) neighborCounter++;
                if (mainBoard[r + 1, c + 1] == Live) neighborCounter++;

            } else if (c == NumCols - 1) {
                //row above
                if (mainBoard[r - 1, c - 1] == Live) neighborCounter++;
                if (mainBoard[r - 1, c] == Live) neighborCounter++;

                //my row
                if (mainBoard[r, c - 1] == Live) neighborCounter++;

                //row below
                if (mainBoard[r + 1, c - 1] == Live) neighborCounter++;
                if (mainBoard[r + 1, c] == Live) neighborCounter++;

            } else if (r == NumRows - 1) {
                //row above
                if (mainBoard[r - 1, c - 1] == Live) neighborCounter++;
                if (mainBoard[r - 1, c] == Live) neighborCounter++;
                if (mainBoard[r - 1, c + 1] == Live) neighborCounter++;

                //my row
                if (mainBoard[r, c - 1] == Live) neighborCounter++;
                if (mainBoard[r, c + 1] == Live) neighborCounter++;

            } else {
                //nominal case 
                //row above
                if (mainBoard[r - 1, c - 1] == Live) neighborCounter++;
                if (mainBoard[r - 1, c] == Live) neighborCounter++;
                if (mainBoard[r - 1, c + 1] == Live) neighborCounter++;

                //my row
                if (mainBoard[r, c - 1] == Live) neighborCounter++;
                if (mainBoard[r, c + 1] == Live) neighborCounter++;

                //row below
                if (mainBoard[r + 1, c - 1] == Live) neighborCounter++;
                if (mainBoard[r + 1, c] == Live) neighborCounter++;
                if (mainBoard[r + 1, c + 1] == Live) neighborCounter++;

            }



            return neighborCounter;
        }


        //Initialize the game board
        public void InitializeGameBoard(char[,] theBoard)
        {
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    theBoard[row, col] = Dead;
                }
            }
        }
        public void PrintGameBoard(char[,] theBoard)
        {
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    Console.Write(theBoard[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

        public void StartupConfigPattern01(int row, int margin)
        {
            //8L
            mainBoard[row, margin + 1] = Live;
            mainBoard[row, margin + 2] = Live;
            mainBoard[row, margin + 3] = Live;
            mainBoard[row, margin + 4] = Live;
            mainBoard[row, margin + 5] = Live;
            mainBoard[row, margin + 6] = Live;
            mainBoard[row, margin + 7] = Live;
            mainBoard[row, margin + 8] = Live;

            //1D =
            //
            //5L =
            mainBoard[row, margin + 10] = Live;
            mainBoard[row, margin + 11] = Live;
            mainBoard[row, margin + 12] = Live;
            mainBoard[row, margin + 13] = Live;
            mainBoard[row, margin + 14] = Live;

            //3D =
            //
            //3L =
            mainBoard[row, margin + 18] = Live;
            mainBoard[row, margin + 19] = Live;
            mainBoard[row, margin + 20] = Live;

            //6D =
            //
            //7L =
            mainBoard[row, margin + 27] = Live;
            mainBoard[row, margin + 28] = Live;
            mainBoard[row, margin + 29] = Live;
            mainBoard[row, margin + 30] = Live;
            mainBoard[row, margin + 31] = Live;
            mainBoard[row, margin + 32] = Live;
            mainBoard[row, margin + 33] = Live;

            //1D = 
            //
            //5L =
            mainBoard[row, margin + 35] = Live;
            mainBoard[row, margin + 36] = Live;
            mainBoard[row, margin + 37] = Live;
            mainBoard[row, margin + 38] = Live;
            mainBoard[row, margin + 39] = Live;

        }
    }
}
