namespace HeroSkin.Utils
{
    public class Layer
    {
        private Pixel[,] pixelGrid;
        private bool visible;
        private int width;
        private int height;

        public Layer(int width, int height)
        {
            this.width = width;
            this.height = height;
            visible = true;
            pixelGrid = new Pixel[width, height];
        }

        public Pixel GetPixel(int x, int y)
        {
            if(x < 0 || x >= width || y < 0 || y >= height)
            {
                return null;
            }
            return pixelGrid[x, y];
        }

        public void SetPixel(int x, int y, Pixel pixel)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                pixelGrid[x, y] = pixel;
            }
        }

        public bool IsVisible()
        {
            return visible;
        }

        public void SetVisible(bool visible)
        {
            this.visible = visible;
        }
    }
}
