namespace Greed.Game.Casting
{
    //creats instances of rocks
    public class Rock : Actor
    {
        private int points = 1;

        
        public Rock()
        {
        }

        
        public int GetPoints()
        {
            return points;
        }

        //if littledude touches rock, -1 point
        public int placeScore(int total)
        {
            total -= points;
            return total;
        }
    }
}