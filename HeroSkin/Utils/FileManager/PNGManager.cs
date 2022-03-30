using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeroSkin.Utils.FileManager
{
    public static class PNGManager
    {
        public static void Save(MainWindow mainWindow, string path)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(mainWindow.PixelEditor.rows, mainWindow.PixelEditor.cols);
            for (int x = 0; x < mainWindow.PixelEditor.rows; x++)
            {
                for (int y = 0; y < mainWindow.PixelEditor.cols; y++)
                {
                    Pixel pixel = mainWindow.PixelEditor.project.GetPixel(x, y);
                    if (pixel != null)
                    {
                        Color color = pixel.GetColor();
                        System.Drawing.Color pixelDrawing = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
                        bitmap.SetPixel(x, y, pixelDrawing);
                    }
                    else
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.Transparent);
                    }
                }
            }
            bitmap.Save(path);
        }

        public static void Load(MainWindow mainWindow, string path, bool newProject = true)
        {
            if(newProject) 
                mainWindow.PixelEditor.Clear();
            else
            {
                mainWindow.PixelEditor.project.AddLayer(mainWindow.PixelEditor.rows, mainWindow.PixelEditor.cols);
                mainWindow.PixelEditor.currentLayer = mainWindow.PixelEditor.project.GetLayerCount() - 1;
                mainWindow.Layers.InitLayers();
            }

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(path);
            for (int x = 0; x < mainWindow.PixelEditor.rows; x++)
            {
                for (int y = 0; y < mainWindow.PixelEditor.cols; y++)
                {
                    System.Drawing.Color pixel = bitmap.GetPixel(x, y);
                    Color pixelMedia = new Color
                    {
                        R = pixel.R,
                        G = pixel.G,
                        B = pixel.B,
                        A = pixel.A
                    };

                    Rectangle pixelToPaint = new Rectangle
                    {
                        Width = mainWindow.PixelEditor.pixelSize,
                        Height = mainWindow.PixelEditor.pixelSize,
                        Fill = new SolidColorBrush(pixelMedia)
                    };
                    Canvas.SetLeft(pixelToPaint, x * mainWindow.PixelEditor.pixelSize);
                    Canvas.SetTop(pixelToPaint, y * mainWindow.PixelEditor.pixelSize);
                    mainWindow.PixelEditor.project.GetLayer(mainWindow.PixelEditor.currentLayer).SetPixel(x, y, new Pixel(pixelMedia));
                    mainWindow.PixelEditor.PixelCanvas.Children.Add(pixelToPaint);
                }
            }
            mainWindow.PixelEditor.UpdateBitmap();
        }
    }
}
