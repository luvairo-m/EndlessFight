using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight
{
    public struct TextureDescription
    {
        public int FrameWidth => Texture.Width / Frames;
        public int Frames;
        public Texture2D Texture;

        public TextureDescription(int frames, Texture2D texture)
        {
            Frames = frames;
            Texture = texture;
        }
    }
}