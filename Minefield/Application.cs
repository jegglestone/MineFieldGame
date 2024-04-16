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

                string message = GetMoveMessage(enumMoveStatusResult);
                Console.WriteLine(message);
                Console.ReadKey();

                DisplayBoard(board, player, mines);

                Console.WriteLine($"Lives: {player.Lives}");
                Console.WriteLine($"Moves: {player.Moves}");
                Console.WriteLine(player.GetPosition());
                Console.Write("Enter direction (up/down/left/right): ");

                string? input = Console.ReadLine()?.ToLower();
                enumMoveStatusResult = _moveHandler.HandleMove(player, mines, input);
            }
        }

        private static string GetMoveMessage(EnumMoveStatusResult enumMoveStatusResult)
        {
            return enumMoveStatusResult switch
            {
                EnumMoveStatusResult.InvalidInput => "Invalid input. Use 'up', 'down', 'left', or 'right'.",
                EnumMoveStatusResult.SteppedOutOfBoundsAttempt => "Can't more there!",
                EnumMoveStatusResult.SteppedInMine => "Oops! You stepped on a mine!",
                EnumMoveStatusResult.SuccessfulMove => "Moved successfully",
                EnumMoveStatusResult.GameOverNoLivesLeft => "Oops! You're dead!",
                EnumMoveStatusResult.ReachedTheOtherSideWinCondition => "Congratulations! You have won",
                _ => "Moved successfully",
            };
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
            InitialiseBoardWithAlternativeSquares(board, player);

            AddMines(board, player, mineCoordinates);

            DisplayChessBoard(board);
        }

        private static void DisplayChessBoard(char[,] board)
        {
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

        private static void AddMines(char[,] board, Player player, List<MineCoordinate> mineCoordinates)
        {
            foreach (var mineCoordinate in mineCoordinates)
            {
                if (IsValidPosition(mineCoordinate.Row, mineCoordinate.Column))
                {
                    if (player.PlayerRow != mineCoordinate.Row || player.PlayerCol != mineCoordinate.Column)
                        board[mineCoordinate.Row, mineCoordinate.Column] = 'M';
                    else board[mineCoordinate.Row, mineCoordinate.Column] = 'P'; // Player is in the mine
                }
            }
        }

        private static void InitialiseBoardWithAlternativeSquares(char[,] board, Player player)
        {
            for (int row = 0; row < ApplicationConstants.BoardSize; row++)
            {
                for (int col = 0; col < ApplicationConstants.BoardSize; col++)
                {
                    board[row, col] = (row + col) % 2 == 0 ? 'x' : 'o';

                    if (row == player.PlayerRow && col == player.PlayerCol)
                        board[row, col] = 'P';
                }
            }
        }

        static bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < ApplicationConstants.BoardSize && col >= 0 && col < ApplicationConstants.BoardSize;
        }
    }
}
