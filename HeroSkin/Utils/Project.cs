using System.Collections.Generic;

namespace HeroSkin.Utils
{
    public class Project
    {
        private List<Layer> layers = new List<Layer>();

        public int GetLayerID(Layer layer)
        {
            return layers.IndexOf(layer);
        }

        public void AddLayer(int width, int height)
        {
            layers.Add(new Layer(width, height));
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
