namespace Greed.Game.Casting
{
    public class Money : Actor
    {
        private int points = 1;

        public Money()
        {

        }

        public int placePoints()
        {
            return points;
        }

        public int placeScore(int total)
        {
            total += points;
            return total;
        }
    }
}