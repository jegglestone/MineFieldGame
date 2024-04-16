using Minefield.Model;

namespace Minefield.Handler
{

    public class MoveHandler : IMoveHandler
    {
        public EnumMoveStatusResult HandleMove(
            Player player, List<MineCoordinate> mines, string? input)
        {
            var enumMoveStatusResult = EnumMoveStatusResult.SuccessfulMove;

            switch (input)
            {
                case "up":
                    if (player.PlayerRow > 0) player.PlayerRow--;
                    else enumMoveStatusResult = EnumMoveStatusResult.SteppedOutOfBoundsAttempt;
                    break;
                case "down":
                    if (player.PlayerRow < ApplicationConstants.BoardSize - 1) player.PlayerRow++;
                    else enumMoveStatusResult = EnumMoveStatusResult.ReachedTheOtherSideWinCondition;
                    break;
                case "left":
                    if (player.PlayerCol > 0) player.PlayerCol--;
                    else enumMoveStatusResult = EnumMoveStatusResult.SteppedOutOfBoundsAttempt;
                    break;
                case "right":
                    if (player.PlayerCol < ApplicationConstants.BoardSize - 1) player.PlayerCol++;
                    else enumMoveStatusResult = EnumMoveStatusResult.SteppedOutOfBoundsAttempt;
                    break;
                default:
                    enumMoveStatusResult = EnumMoveStatusResult.InvalidInput;
                    break;
            }

            enumMoveStatusResult = HandleMines(player, mines, enumMoveStatusResult);

            if (player.Lives == 0)
            {
                enumMoveStatusResult = EnumMoveStatusResult.GameOverNoLivesLeft;
            }

            player.Moves++;
            return enumMoveStatusResult;
        }

        private static EnumMoveStatusResult HandleMines(Player player, List<MineCoordinate> mines, EnumMoveStatusResult enumMoveStatusResult)
        {
            foreach (var mine in mines)
            {
                if (mine.Row == player.PlayerRow && mine.Column == player.PlayerCol)
                {
                    player.Lives--;
                    enumMoveStatusResult = EnumMoveStatusResult.SteppedInMine;
                    break;
                }
            }

            return enumMoveStatusResult;
        }
    }
}
