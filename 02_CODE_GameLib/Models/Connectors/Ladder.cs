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

        public Position GetCorrectPosition(Direction side)
        {
            return side == Direction.UPPER ? Upper : Lower;
        }
    }
}