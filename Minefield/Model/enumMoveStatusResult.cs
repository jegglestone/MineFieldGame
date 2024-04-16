namespace Minefield.Model
{
    public enum EnumMoveStatusResult
    {
        InitialMove,
        SuccessfulMove,
        SteppedInMine,
        SteppedOutOfBoundsAttempt,
        ReachedTheOtherSideWinCondition,
        GameOverNoLivesLeft,
        InvalidInput
    }
}