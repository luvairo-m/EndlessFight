using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace EndlessFight
{
    public class SpriteManager
    {
        protected Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        public Size Size;
        public int FrameWidth;
        protected Rectangle[] Rectangles;
        public int FrameIndex = 0;

        public SpriteManager(Texture2D texture, int frames)
        {
            Texture = texture;
            Rectangles = new Rectangle[frames];

            var frameWidth = Texture.Width / frames;
            Size = new(frameWidth, Texture.Height);

            for (var i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(i * frameWidth, 0, frameWidth, Texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch) =>
            spriteBatch.Draw(Texture, Position, Rectangles[FrameIndex],
                Color, Rotation, Origin, Scale, SpriteEffect, 0f);
    }

    public class SpriteAnimation : SpriteManager
    {
        private float timeElapsed;
        public bool IsLooping = true;
        public int FramesPerSecond { set { timeToUpdate = 1f / value; } }
        private float timeToUpdate;

        public SpriteAnimation(Texture2D Texture, int frames, int fps) : base(Texture, frames)
            => FramesPerSecond = fps;

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                if (FrameIndex < Rectangles.Length - 1) FrameIndex++;
                else if (IsLooping) FrameIndex = 0;
            }
        }

        public void SetFrame(int frame) => FrameIndex = frame;
    }
}