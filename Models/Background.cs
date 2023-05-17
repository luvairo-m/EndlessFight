using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EndlessFight.Models
{
    public class Star
    {
        public int Size { get; private set; }
        public Color Color { get; private set; }
        public Vector2 Position => position;

        private Vector2 position;
        private readonly int speed;

        public Star(int speed, int size, Vector2 position, Color color)
        {
            Size = size;
            Color = color;
            this.speed = speed;
            this.position = position;
        }

        public void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += delta * speed;
        }
    }

    public class Background
    {
        public Texture2D StarTexture { get; private set; }

        private readonly List<Star> stars = new();
        private readonly Color color;
        private const int spawnOffset = -15;

        private float frequency;
        private readonly float frequencyBuffer;

        public Background(Texture2D backgroundTexture, Color backgroundColor, float spawnFrequency)
        {
            StarTexture = backgroundTexture;
            frequencyBuffer = spawnFrequency;
            color = backgroundColor;
            frequency = spawnFrequency;
        }

        public void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frequency -= delta;

            if (frequency <= 0)
            {
                for (var i = 0; i < 6; i++)
                {
                    var size = Globals.Randomizer.Next(2, 6);
                    var color = Globals.Randomizer.Next(0, 2) == 1 ? Color.White : Color.Gray;

                    stars.Add(new Star(Globals.Randomizer.Next(200, 300), size,
                        new(Globals.Randomizer.Next(spawnOffset, Game1.windowWidth), 0), color));
                }

                frequency = frequencyBuffer;
            }

            stars.ForEach(star => star.Update(gameTime));

            for (var i = 0; i < stars.Count; i++)
                if (stars[i].Position.Y > Game1.windowHeight)
                    stars.RemoveAt(i);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
        {
            device.Clear(color);
            stars.ForEach(star =>
                spriteBatch.Draw(StarTexture, 
                new Rectangle((int)star.Position.X, (int)star.Position.Y, star.Size, star.Size),
                star.Color));
        }
    }
}