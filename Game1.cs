﻿using EndlessFight.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceBattle.GameStates;

namespace EndlessFight
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        #region Window Size
        public const int windowWidth = 750;
        public const int windowHeight = 900;
        #endregion

        #region Game Field Offset
        public const int fieldOffset = 20;
        #endregion

        #region State
        private State currentState;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            currentState = new GameState(this, Content, graphics);
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
            currentState.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}