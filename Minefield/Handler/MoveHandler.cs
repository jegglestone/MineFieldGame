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
                    break;
                case "down":
                    if (player.PlayerRow < ApplicationConstants.BoardSize - 1) player.PlayerRow++;
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

            // Check for mines and update lives/moves
            foreach (var mine in mines)
            {
                if (mine.Row == player.PlayerRow && mine.Column == player.PlayerCol)
                {
                    player.Lives--;
                    enumMoveStatusResult = EnumMoveStatusResult.SteppedInMine;
                    break;
                }
            }

            // Check if player reached the other side (win condition)
            if (player.PlayerRow == ApplicationConstants.BoardSize - 1)
            {
                return EnumMoveStatusResult.ReachedTheOtherSideWinCondition;
            }

            // Handle game over (no lives left)
            if (player.Lives == 0)
            {
                return EnumMoveStatusResult.GameOverNoLivesLeft;
            }

            player.Moves++;
            return enumMoveStatusResult;
        }
    }
}
