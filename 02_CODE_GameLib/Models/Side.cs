using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
    public enum Side
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    public static class SideExtension
    {
        public static Position GetStep(this Side direction)
        {
            switch (direction)
            {
                case Side.NORTH:
                    return new Position { X = 0, Y = -1 };
                case Side.EAST:
                    return new Position { X = 1, Y = 0 };
                case Side.SOUTH:
                    return new Position { X = 0, Y = 1 };
                case Side.WEST:
                    return new Position { X = -1, Y = 0 };
                default:
                    return new Position { X = 0, Y = 0 };
            }
        }

        public static Side GetOpposite(this Side direction)
        {
            switch (direction)
            {
                case Side.NORTH:
                    return Side.SOUTH;
                case Side.EAST:
                    return Side.WEST;
                case Side.SOUTH:
                    return Side.NORTH;
                case Side.WEST:
                    return Side.EAST;
                default:
                    return Side.SOUTH;
            }
        }

        public static Side GetByName(this string direction)
        {
            return (Side)System.Enum.Parse(typeof(Side), direction);
        }
    }
}