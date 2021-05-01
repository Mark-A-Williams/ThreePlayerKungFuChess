namespace Core.Models
{
    public class Position
    {
        public char TransversePosition { get; set; }
        public int LongitudinalPosition { get; set; }

        public override string ToString()
        {
            return $"{TransversePosition}{LongitudinalPosition}";
        }
    }
}
