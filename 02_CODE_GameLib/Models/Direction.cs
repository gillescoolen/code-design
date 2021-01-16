using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
    public enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST,
        UPPER,
        LOWER
    }

    public static class DirectionExtension
    {
        public static Position GetStep(this Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH:
                    return new Position { X = 0, Y = -1 };
                case Direction.EAST:
                    return new Position { X = 1, Y = 0 };
                case Direction.SOUTH:
                    return new Position { X = 0, Y = 1 };
                case Direction.WEST:
                    return new Position { X = -1, Y = 0 };
                default:
                    return new Position { X = 0, Y = 0 };
            }
        }

        public static Direction GetOpposite(this Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH:
                    return Direction.SOUTH;
                case Direction.EAST:
                    return Direction.WEST;
                case Direction.SOUTH:
                    return Direction.NORTH;
                case Direction.WEST:
                    return Direction.EAST;
                case Direction.UPPER:
                    return Direction.LOWER;
                case Direction.LOWER:
                    return Direction.UPPER;
                default:
                    return Direction.SOUTH;
            }
        }

        public static Direction GetByName(this string direction)
        {
            return (Direction)System.Enum.Parse(typeof(Direction), direction);
        }
    }
}