using FluentAssertions;
using Minefield.Handler;
using Minefield.Model;

namespace MineField.UnitTests
{
    public class MoveHandlerTests
    {
        private readonly IMoveHandler _moveHandler;

        public MoveHandlerTests()
        {
            _moveHandler = new MoveHandler();
        }

        [Theory]
        [InlineData("down")]
        [InlineData("up")]
        [InlineData("left")]
        [InlineData("right")]
        public void When_MakesMove_IncrementsMoveCount(string move)
        {
            var player = new Player
            {
                Moves = 0,
            };

            var mines = new List<MineCoordinate>
            {
            };

            _moveHandler.HandleMove(player, mines, move);

            player.Moves.Should().Be(1);
        }

        [Fact]
        public void When_SteppedOnMine_LosesALife()
        {
            var player = new Player
            {
                Lives = 4,
                PlayerCol = 1,
                PlayerRow = 1
            };

            var mines = new List<MineCoordinate>
            {
                new MineCoordinate(row: 2, col: 1)
            };

            var result = _moveHandler.HandleMove(player, mines, "down");

            player.Lives.Should().Be(3);
            result.Should().Be(EnumMoveStatusResult.SteppedInMine);
        }

        [Fact]
        public void When_ReachesOtherSide_Wins()
        {
            var player = new Player
            {
                Lives = 1,
                PlayerCol = 1,
                PlayerRow = 7
            };

            var mines = new List<MineCoordinate>
            {
            };

            var result = _moveHandler.HandleMove(player, mines, "down");

            result.Should().Be(EnumMoveStatusResult.ReachedTheOtherSideWinCondition);
        }

        [Fact]
        public void When_LastLifeTaken_LosesGame()
        {
            var player = new Player
            {
                Lives = 1,
                PlayerCol = 1,
                PlayerRow = 1
            };

            var mines = new List<MineCoordinate>
            {
                new MineCoordinate(row: 2, col: 1)
            };

            var result = _moveHandler.HandleMove(player, mines, "down");

            result.Should().Be(EnumMoveStatusResult.GameOverNoLivesLeft);
        }

        [Fact]
        public void When_InvalidInput_ReturnsInvalidInputAttempt()
        {
            var player = new Player
            {
                Lives = 1,
                PlayerCol = 1,
                PlayerRow = 1
            };

            var mines = new List<MineCoordinate>
            {
            };

            var result = _moveHandler.HandleMove(player, mines, "sideways");

            result.Should().Be(EnumMoveStatusResult.InvalidInput);
        }

        [Fact]
        public void When_MovingOutsideLeftBounds_ReturnsSteppedOutOfBoundsAttempt()
        {
            var player = new Player
            {
                Lives = 1,
                PlayerCol = 0,
                PlayerRow = 0
            };

            var mines = new List<MineCoordinate>
            {
            };

            var result = _moveHandler.HandleMove(player, mines, "left");

            result.Should().Be(EnumMoveStatusResult.SteppedOutOfBoundsAttempt);
        }

        [Fact]
        public void When_MovingOutsideRightBounds_ReturnsSteppedOutOfBoundsAttempt()
        {
            var player = new Player
            {
                Lives = 1,
                PlayerCol = 7,
                PlayerRow = 0
            };

            var mines = new List<MineCoordinate>
            {
            };

            var result = _moveHandler.HandleMove(player, mines, "right");

            result.Should().Be(EnumMoveStatusResult.SteppedOutOfBoundsAttempt);
        }
    }
}
