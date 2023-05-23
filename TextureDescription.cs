using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight
{
    public struct TextureDescription
    {
        public int FrameWidth => Texture.Width / Frames;
        public int Frames;
        public Texture2D Texture;

        public TextureDescription(Texture2D texture, int frames)
        {
            Texture = texture;
            Frames = frames;
        }
    }
}