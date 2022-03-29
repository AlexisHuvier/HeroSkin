using System.IO;
using System.Text;
using System.Windows.Media;

namespace HeroSkin.Utils.FileManager
{
    public static class HSProjManager
    {
        public static void Save(MainWindow mainWindow, string path)
        {
            using FileStream stream = File.Open(path, FileMode.Create);
            using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false);
            writer.Write(mainWindow.PixelEditor.project.GetLayerCount());
            for(int i = 0; i < mainWindow.PixelEditor.project.GetLayerCount(); i++)
            {
                Layer layer = mainWindow.PixelEditor.project.GetLayer(i);
                writer.Write(layer.name);
                writer.Write(layer.width);
                writer.Write(layer.height);
                writer.Write(layer.IsVisible());
                for (int x = 0; x < layer.width; x++)
                {
                    for(int y = 0; y < layer.height; y++)
                    {
                        Pixel pixel = layer.GetPixel(x, y);
                        if(pixel == null)
                        {
                            writer.Write(false);
                        }
                        else
                        {
                            Color color = pixel.GetColor();
                            writer.Write(true);
                            writer.Write(color.R);
                            writer.Write(color.G);
                            writer.Write(color.B);
                            writer.Write(color.A);
                        }
                    }
                }
            }
        }

        public static void Load(MainWindow mainWindow, string path)
        {
            using FileStream stream = File.Open(path, FileMode.Open);
            using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false);
            mainWindow.PixelEditor.Clear();
            mainWindow.PixelEditor.project.RemoveLayer(0);
            int nbLayer = reader.ReadInt32();
            for (int i = 0; i < nbLayer; i++)
            {
                Layer layer = new Layer(reader.ReadString(), reader.ReadInt32(), reader.ReadInt32());
                layer.SetVisible(reader.ReadBoolean());
                for (int x = 0; x < layer.width; x++)
                {
                    for (int y = 0; y < layer.height; y++)
                    {
                        if(reader.ReadBoolean())
                        {
                            byte R = reader.ReadByte();
                            byte G = reader.ReadByte();
                            byte B = reader.ReadByte();
                            byte A = reader.ReadByte();
                            Pixel pixel = new Pixel(Color.FromArgb(A, R, G, B));
                            layer.SetPixel(x, y, pixel);
                        }
                    }
                }
                mainWindow.PixelEditor.project.layers.Add(layer);
            }

            mainWindow.Layers.InitLayers();
        }
    }
}
