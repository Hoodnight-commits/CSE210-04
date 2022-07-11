using System.Collections.Generic;
using Greed.Game.Casting;
using Greed.Game.Services;
using System.IO;
using System.Linq;

namespace Greed.Game.Director
{
    // The responsibility of a Director is to control the sequence of play.

    public class Director
    {
        public int score = 0;
        private KeyboardServices keyboardService = null;
        private VideoServices videoService = null;

        // Constructs a new instance of Director using the given KeyboardServices and VideoServices.
        public Director(KeyboardServices keyboardService, VideoServices videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }


        // Starts the game by running the main game loop.
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }
        private void moveMoneysandRocks(Cast cast)
        {
            List<Actor> rocks = cast.GetActors("rocks");
            List<Actor> Moneys = cast.GetActors("Moneys");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            foreach (Actor actor in rocks)
            {
                actor.movement(maxX, maxY);
            }
            
            foreach (Actor actor in Moneys)
            {
                actor.movement(maxX, maxY);
            }
        }
        private void HandleCollision(Cast cast)
        {
            List<Actor> rocks = cast.GetActors("rocks");
            List<Actor> Moneys = cast.GetActors("Moneys");
            Actor littledude = cast.GetFirstActor("littledude");
            Actor banner = cast.GetFirstActor("banner");
            foreach (Actor actor in rocks)
            {
                //handles the score if the littledude touches a rock
                if (littledude.getPlacement().Equals(actor.getPlacement()))
                {
                    Rock rock = (Rock) actor;
                    System.Console.WriteLine(banner.getText());
                    int BannerAsINT = int.Parse(banner.getText());
                    int NewTotal = rock.placeScore(BannerAsINT);
                    string NewTotalAsString = NewTotal.ToString();
                    banner.placeText(NewTotalAsString);
                    cast.RemoveActor("rocks", actor);
                }
            }
            foreach (Actor actor in Moneys)
            {
                //handles the score if the littledude touches a Money
                if (littledude.getPlacement().Equals(actor.getPlacement()))
                {
                    Money money = (Money) actor;
                    System.Console.WriteLine(banner.getText());
                    int BannerAsINT = int.Parse(banner.getText());
                    int NewTotal = money.placeScore(BannerAsINT);
                    string NewTotalAsString = NewTotal.ToString();
                    banner.placeText(NewTotalAsString);
                    cast.RemoveActor("Moneys", actor);
                }
            }
        }
        private void HandleOutOfScreenActors(Cast cast)
        {
            List<Actor> rocks = cast.GetActors("rocks");
            List<Actor> moneys = cast.GetActors("Moneys");
            int maxY = videoService.GetHeight();
            foreach (Actor actor in rocks)
            {
                
                if (actor.getPlacement().getY() > maxY)
                {
                    cast.RemoveActor("rocks", actor);
                }
            }
            foreach (Actor actor in moneys)
            {
                
                if (actor.getPlacement().getY() > maxY)
                {
                    cast.RemoveActor("Moneys", actor);
                }
            }
        }

        private void SpawnNewRocksAndMoneys(Cast cast)
        {
            
            System.Random random = new System.Random();
            // I don't think that it's good to have the game make a new rock and Money each game loop
            // So I'm just gonna make it a new random value that determines if we are going to generate rocks and Moneys or not
            int randoNumber = random.Next(0, 8);
            if (randoNumber == 1)
            {
                for (int i = 0; i < 1; i++)
                {
                    int x = random.Next(1, Program.COLS);
                    int y = 0;
                    Point position = new Point(x, y);
                    position = position.Scale(Program.CELL_SIZE);

                    int r = random.Next(0, 256);
                    int g = random.Next(0, 256);
                    int b = random.Next(0, 256);
                    Color color = new Color(r, g, b);

                    Rock rock = new Rock();
                    rock.placeText("0");
                    rock.placeFontSize(Program.FONT_SIZE);
                    rock.placeVelocity(new Point(0, 1));
                    rock.placeColor(color);
                    rock.placePlacement(position);
                    cast.AddActor("rocks", rock);

                }
            }

            if (randoNumber == 2)
            {
                for (int i = 0; i < 1; i++)
                {
                    int x = random.Next(1, Program.COLS);
                    int y = 0;
                    Point position = new Point(x, y);
                    position = position.Scale(Program.CELL_SIZE);

                    int r = random.Next(0, 256);
                    int g = random.Next(0, 256);
                    int b = random.Next(0, 256);
                    Color color = new Color(r, g, b);

                    Money Money = new Money();
                    Money.placeText("$");
                    Money.placeFontSize(Program.FONT_SIZE);
                    Money.placeVelocity(new Point(0, 1));
                    Money.placeColor(color);
                    Money.placePlacement(position);
                    cast.AddActor("Moneys", Money);
                }
            }
        }
        // Gets directional input from the keyboard and applies it to the littledude.
        private void GetInputs(Cast cast)
        {
            Actor littledude = cast.GetFirstActor("littledude");
            Point velocity = keyboardService.GetDirection();
            littledude.placeVelocity(velocity);     
        }

        // Updates the littledude's position and resolves any collisions with the money or rocks.
        private void DoUpdates(Cast cast)
        {
            //Actor banner = cast.GetFirstActor("banner");
            Actor littledude = cast.GetFirstActor("littledude");
            
            List<Actor> rocks = cast.GetActors("rocks");
            List<Actor> Moneys = cast.GetActors("Moneys");

            //banner.SetNewScore(score, );
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            littledude.movement(maxX, maxY);
           
            moveMoneysandRocks(cast);
            HandleCollision(cast);
            HandleOutOfScreenActors(cast);
            SpawnNewRocksAndMoneys(cast);
            //System.Console.WriteLine(score);
        }

        // Draws the actors on the screen.
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }

    }
}