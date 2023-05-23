using EndlessFight;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.GameStates
{
    public abstract class State
    {
        protected ContentManager ContentManager { get; }
        protected GraphicsDeviceManager Graphics { get; }
        protected Game1 Game { get; }

        public State(Game1 game, ContentManager contentManager, GraphicsDeviceManager graphics)
        {
            Game = game;
            ContentManager = contentManager;
            Graphics = graphics;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
        public abstract void LoadContent();
        public abstract void Initialize();
    }
}