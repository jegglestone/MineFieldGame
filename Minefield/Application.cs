using Minefield.Handler;
using Minefield.Model;

namespace MinesweeperGame
{
    public class Application
    {
        const int NumberOfMines = 6;

        private readonly IMoveHandler _moveHandler;

        public Application()
        {
            _moveHandler = new MoveHandler();
        }

        public void PlayGame()
        {
            char[,] board = new char[ApplicationConstants.BoardSize, ApplicationConstants.BoardSize];
            var player = new Player();
            var mines = CreateMines();
            var enumMoveStatusResult = EnumMoveStatusResult.SuccessfulMove;

            while (true)
            {
                Console.Clear();

                DisplayMoveMessages(enumMoveStatusResult);

                // Testing purpose - display option
                DisplayBoard(board, player, mines);

                //display moves, lives
                Console.WriteLine($"Lives: {player.Lives}");
                Console.WriteLine($"Moves: {player.Moves}");
                Console.WriteLine(player.GetPosition());
                Console.Write("Enter direction (up/down/left/right): ");

                string input = Console.ReadLine().ToLower();
                enumMoveStatusResult = _moveHandler.HandleMove(player, mines, input);
            }
        }

        private static void DisplayMoveMessages(EnumMoveStatusResult enumMoveStatusResult)
        {
            switch (enumMoveStatusResult)
            {
                case EnumMoveStatusResult.SteppedOutOfBoundsAttempt:
                    Console.WriteLine("Invalid input. Use 'up', 'down', 'left', or 'right'.");
                    Console.ReadKey();
                    break;
                case EnumMoveStatusResult.SteppedInMine:
                    Console.WriteLine("Oops! You stepped on a mine!");
                    Console.ReadKey();
                    break;
                case EnumMoveStatusResult.GameOverNoLivesLeft:
                    Console.WriteLine("Oops! You're dead!");
                    Console.ReadKey();
                    break;
                case EnumMoveStatusResult.ReachedTheOtherSideWinCondition:
                    Console.WriteLine("Congratulations! You have won");
                    Console.ReadKey();
                    break;
            }
        }

        private static List<MineCoordinate> CreateMines()
        {
            Random random = new();
            List<MineCoordinate> minesCoords = new();
            for (int i = 0; i < NumberOfMines; i++)
            {
                int mineRow = random.Next(ApplicationConstants.BoardSize);
                int mineCol = random.Next(ApplicationConstants.BoardSize);

                minesCoords.Add(new MineCoordinate(mineRow, mineCol));
            }

            return minesCoords;
        }

        static void DisplayBoard(char[,] board, Player player, List<MineCoordinate> mineCoordinates)
        {
            // Initialize the board with alternate squares
            for (int row = 0; row < ApplicationConstants.BoardSize; row++)
            {
                for (int col = 0; col < ApplicationConstants.BoardSize; col++)
                {
                    board[row, col] = (row + col) % 2 == 0 ? 'x' : 'o';

                    if (row == player.PlayerRow && col == player.PlayerCol)
                        board[row, col] = 'P';
                }
            }

            // Add mines
            foreach (var mineCoordinate in mineCoordinates)
            {
                if (IsValidPosition(mineCoordinate.Row, mineCoordinate.Column))
                {
                    if (player.PlayerRow != mineCoordinate.Row || player.PlayerCol != mineCoordinate.Column)
                        board[mineCoordinate.Row, mineCoordinate.Column] = 'M';
                    else board[mineCoordinate.Row, mineCoordinate.Column] = 'P'; // Player is in the mine
                }
            }

            // Display the chessboard
            Console.WriteLine("   A B C D E F G H"); // Letters along the bottom
            for (int row = 0; row < ApplicationConstants.BoardSize; row++)
            {
                Console.Write($"{row + 1} "); // Numbers up the side
                for (int col = 0; col < ApplicationConstants.BoardSize; col++)
                {
                    Console.Write($"{board[row, col]} ");
                }
                Console.WriteLine();
            }
        }

        static bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < ApplicationConstants.BoardSize && col >= 0 && col < ApplicationConstants.BoardSize;
        }
    }
}
