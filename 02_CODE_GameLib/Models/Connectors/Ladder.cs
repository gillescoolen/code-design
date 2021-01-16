using System;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Connectors
{
    public class Ladder : IRenderable
    {
        private Position Upper { get; }
        private Position Lower { get; }
        public virtual ConsoleColor Color { get; set; } = ConsoleColor.White;

        public Ladder(Position upper, Position lower)
        {
            Upper = upper;
            Lower = lower;
        }

        public Position GetPosition(Direction direction)
        {
            return direction == Direction.UPPER ? Upper : Lower;
        }
    }
}