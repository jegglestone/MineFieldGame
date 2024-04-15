using Minefield.Model;

namespace Minefield.Handler
{
    public interface IMoveHandler
    {
        public EnumMoveStatusResult HandleMove(
            Player player, List<MineCoordinate> mines, string input);
    }
}
