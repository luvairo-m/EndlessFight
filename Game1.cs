using EndlessFight.GameStates;
using EndlessFight.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static EndlessFight.Resources;

namespace EndlessFight
{
    public class Game1 : Game
    {
        private State menuState;
        private State gameState;

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

            Globals.MainGame = this;

            menuState = new MenuState(this, Content, graphics);
            gameState = new GameState(this, Content, graphics);
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
            currentBackground = new Background(BackgroundTexture, 1f);
            currentState = menuState;
            currentState.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);

            if (!GameState.IsPaused && !GameState.IsGameOver)
                currentBackground.Update();

            currentState.Update(gameTime);
            Globals.StartGamePulsation.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin(sortMode: SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
            currentBackground.Draw(spriteBatch, graphics.GraphicsDevice);

            //if (GameState.IsPaused)
            //{
            //    spriteBatch.Draw(BackgroundTexture, new Rectangle(0, 0, windowWidth, windowHeight),
            //        Color.FromNonPremultiplied(0, 0, 0, 185));
            //}

            currentState.Draw(gameTime, spriteBatch);
            Globals.StartGamePulsation.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ChangeState()
        {
            currentState.OnExit();
            if (currentState is MenuState) currentState = gameState;
            else currentState = menuState;
            currentState.LoadContent();
        }
    }
}