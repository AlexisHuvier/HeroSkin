using System.Collections.Generic;

namespace HeroSkin.Utils
{
    public class Project
    {
        public List<Layer> layers = new List<Layer>();
        private int layerCount = 0;

        public Pixel GetPixel(int x, int y)
        {
            Pixel pixel = null;
            foreach (Layer layer in layers) {
                if (layer.IsVisible() && layer.GetPixel(x, y) != null && layer.GetPixel(x, y).GetColor() != System.Windows.Media.Colors.Transparent)
                    pixel = layer.GetPixel(x, y);
            };
            return pixel;
        }

        public int GetLayerID(Layer layer)
        {
            return layers.IndexOf(layer);
        }

        public void AddLayer(int width, int height)
        {
            layers.Add(new Layer($"Layer N°{layerCount + 1}", width, height));
            layerCount++;
        }

        public void RemoveLayer(int layerId)
        {
            layers.RemoveAt(layerId);
        }

        public Layer GetLayer(int layerId)
        {
            return layers[layerId];
        }

        public int GetLayerCount()
        {
            return layers.Count;
        }
    }
}
