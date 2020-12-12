namespace CODE_GameLib.Models.Doors
{
    public class ToggleDoor : Door
    {
        public override string Color { get; set; } = "white";

        public override bool IsInteractable(Player player)
        {
            return Open;
        }
    }
}