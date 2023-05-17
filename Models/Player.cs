using EndlessFight.Controllers;
using EndlessFight.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EndlessFight.Models
{
    public class Player : IMovable, IAnimatable, IHittable
    {
        public int ShootingMultiplier = 3;
        public static int CurrentLifes = 5;

        public Vector2 Position { get => position; set => position = value; }
        public Rectangle HitBox => new((int)Position.X, (int)Position.Y,
            Globals.PlayerShipSize, Globals.PlayerShipSize);
        public int Speed { get => speed; set => speed = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }

        private Vector2 position;
        private int speed;
        private bool isAlive = true;

        private Rectangle sourceRectangle;
        private readonly SpriteAnimation exhaustAnimation;
        private readonly Texture2D shipTexture;

        // В разработке
        private readonly Texture2D missileTexture;

        #region Blaster bullet
        private readonly Texture2D blasterTexture;
        #endregion

        private bool isShooting;

        public Player(Vector2 spawnPosition, int speed, Texture2D shipTexture,
            Texture2D missileTexture, Texture2D blasterTexture, SpriteAnimation exhaustAnimation)
        {
            Position = spawnPosition;
            sourceRectangle = new Rectangle(16, 0, 16, shipTexture.Height);
            Speed = speed;
            this.blasterTexture = blasterTexture;
            this.exhaustAnimation = exhaustAnimation;
            this.shipTexture = shipTexture;
            this.missileTexture = missileTexture;
        }

        public void Update(GameTime gameTime)
        {
            HandleMovement(gameTime);
            HandleAnimation(gameTime);
            HandleShooting();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            exhaustAnimation.Draw(spriteBatch);
            spriteBatch.Draw(shipTexture, HitBox, sourceRectangle, Color.White);
        }

        public void HandleMovement(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var direction = InputController.GetMovementDirectionFromInput();

            if (direction == Direction.Left && position.X > Game1.fieldOffset)
            {
                sourceRectangle.X = 0;
                position.X -= Speed * delta;
            }
            else if (direction == Direction.Right
                && position.X + Globals.PlayerShipSize < Game1.windowWidth - Game1.fieldOffset)
            {
                sourceRectangle.X = 32;
                position.X += Speed * delta;
            }
            else if (direction == Direction.Up
                && position.Y > Game1.fieldOffset)
            {
                sourceRectangle.X = 16;
                position.Y -= Speed * delta;
            }
            else if (direction == Direction.Down
                && position.Y + Globals.PlayerShipSize < Game1.windowHeight - Game1.fieldOffset)
            {
                sourceRectangle.X = 16;
                position.Y += Speed * delta;
            }
            else sourceRectangle.X = 16;
        }

        public void HandleAnimation(GameTime gameTime)
        {
            exhaustAnimation.Update(gameTime);
            exhaustAnimation.Position = new(Position.X + Globals.PlayerShipSize / 4,
                Position.Y + Globals.PlayerShipSize);
        }

        private void HandleShooting()
        {
            var shootType = InputController.GetBulletTypeFromInput();
            if (shootType == BulletType.Blaster) DoBlasterShoot();
            else if (shootType == BulletType.Rocket) DoRocketShoot();
            else isShooting = false;
        }

        private void DoRocketShoot()
        {
            throw new NotImplementedException();
        }

        private void DoBlasterShoot()
        {
            if (!isShooting)
            {
                float step = (float)Globals.PlayerShipSize / (ShootingMultiplier * 2);
                var buffer = Position.X;

                for (var i = 0; i < ShootingMultiplier; i++)
                {
                    var placement = buffer + step;
                    buffer = placement + step;

                    BulletsController.CurrentBullets.Add(new Bullet(
                        new(placement - blasterTexture.Width * Globals.BlasterScale / 2, Position.Y),
                        700, new(0, -1), new(blasterTexture, 1, 1) { Scale = Globals.BlasterScale }, BulletOwner.Player));
                }

                isShooting = true;
            }
        }
    }
}