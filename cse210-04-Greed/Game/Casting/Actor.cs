using System;


namespace Greed.Game.Casting
{


    public class Actor
    {
        private string text = "";
        private int fontSize = 15;
        private Color color = new Color(255, 255, 255);
        private Point placement = new Point(0, 0);
        private Point velocity = new Point(0, 0);

        
        private int score;


        public Actor()
        {

        }


        public Color getColor()
        {
            return color;
        }

        public int getFontSize()
        {
            return fontSize;
        }

        public Point getPlacement()
        {
            return placement;
        }

        public string getText()
        {
            return text;
        }

        public Point getVelocity()
        {
            return velocity;
        }

        public void movement(int maxX, int maxY)
        {
            int x = ((placement.getX() + velocity.getX()) + maxX) % maxX;
            int y = (placement.getY() + velocity.getY());
            placement = new Point(x, y);
        }


        public void placeColor(Color color)
        {
            if (color == null)
            {
                throw new ArgumentException("Color can't be null");
            }
            this.color = color;
        }

        public void placeFontSize( int fontSize)
        {
            if (fontSize <=0)
            {
                throw new ArgumentException("Fontsize must be greater than zero");
            }
            this.fontSize = fontSize;
        }

        public void placePlacement(Point placement)
        {
            if (placement == null)
            {
                throw new ArgumentException("Position can't be null");
            }
            this.placement = placement;
        }

        public void placeText(string text)
        {
            if (text == null)
            {
                throw new ArgumentException("text can't be null");
            }
            this.text = text;
        }

        public void setNewScore(int score, int input)
        {
            score += input;
        }        

        public void placeVelocity(Point velocity)
        {
            if (velocity == null)
            {
                throw new ArgumentException("velocity can't be null");
            }
            this.velocity = velocity;
        }
    }
}