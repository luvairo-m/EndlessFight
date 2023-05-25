using Microsoft.Xna.Framework.Input;

namespace EndlessFight.Controllers
{
    public enum Direction { Left, Right, Up, Down, None }
    public enum BulletType { Blaster, None }

    public class InputController
    {
        public static Direction GetMovementDirectionFromInput()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                return Direction.Left;
            else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                return Direction.Right;
            else if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                return Direction.Up;
            else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                return Direction.Down;
            else
                return Direction.None;
        }

        public static BulletType GetBulletTypeFromInput()
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space)) return BulletType.Blaster;
            else return BulletType.None;
        }
    }
}