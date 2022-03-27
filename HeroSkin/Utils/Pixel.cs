using System.Windows.Media;

namespace HeroSkin.Utils
{
    public class Pixel
    {
        private Color color = Colors.Transparent;

        public Pixel() {}

        public Pixel(Color color)
        {
            this.color = color;
        }

        public Color GetColor()
        {
            return color;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }
    }
}
