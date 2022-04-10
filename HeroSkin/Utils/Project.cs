using System.Collections.Generic;
using System.Windows.Media;

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

        public void ModifySaturation(float value)
        {
            foreach(Layer layer in layers)
            {
                for(int x = 0; x < layer.width; x++)
                {
                    for (int y = 0; y < layer.height; y++)
                    {

                        Pixel pixel = layer.GetPixel(x, y);
                        if(pixel != null)
                        {
                            Color pixelColor = pixel.GetColor();
                            if (pixelColor.A > 0)
                            {
                                float[] hslColor = ColorUtils.GetHSLFromColor(pixelColor);
                                hslColor[1] += value;
                                if (hslColor[1] < 0)
                                    hslColor[1] = 0;
                                else if (hslColor[1] > 1)
                                    hslColor[1] = 1;
                                pixel.SetColor(ColorUtils.GetColorFromHSLWithAlpha(hslColor, pixelColor.A));
                            }
                        }
                    }
                }
            }
        }
    }
}
