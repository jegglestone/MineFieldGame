namespace Minefield.Model
{
    public class Player
    {
        public int PlayerRow { get; set; } = 0;
        public int PlayerCol { get; set; } = 0;
        public int Lives { get; set; } = 4;
        public int Moves { get; set; } = 0;

        public string GetPosition()
        {
            char columnLetter = (char)('A' + PlayerCol);
            int rowNumber = PlayerRow + 1;

            return $"{columnLetter}{rowNumber}";
        }
    }
}
