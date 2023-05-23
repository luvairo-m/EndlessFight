using EndlessFight.GameStates;
using EndlessFight.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static EndlessFight.Resources;

namespace EndlessFight
{
    public class Game1 : Game
    {
        #region Window Size
        public const int windowWidth = 750;
        public const int windowHeight = 900;
        #endregion

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Resources gameResources;

        #region State
        private static State currentState;
        #endregion

        #region Background
        private Background currentBackground;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            gameResources = new(Content);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 750;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameResources.InitializeResources();
            currentBackground = new Background(BackgroundTexture, Color.FromNonPremultiplied(20, 20, 20, 255), 1f);
            currentState = new MenuState(this, Content, graphics);
            currentState.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            currentBackground.Update();
            currentState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);

            currentBackground.Draw(spriteBatch, graphics.GraphicsDevice);
            currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);

            spriteBatch.End();
        }

        public void ChangeState(State state)
        {
            state.LoadContent();
            currentState = state;
        }
    }
}