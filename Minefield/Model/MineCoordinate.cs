namespace Minefield.Model
{
    public class MineCoordinate
    {
        public MineCoordinate(int row, int col)
        {
            Row = row;
            Column = col;
        }

        public int Row { get; }
        public int Column { get; }
    }
}
