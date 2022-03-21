using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeroSkin.Utils.Tools
{
    class NoiseBrush : ITool
    {
        public void PaintPixel(Elements.PixelEditor pixelEditor, SolidColorBrush brush, int brushSize, bool isRectForm, int forceBrush)
        {
            Point mousePos = Mouse.GetPosition(pixelEditor.PixelCanvas);
            int rowPos = (int)(mousePos.X / pixelEditor.pixelSize);
            int colPos = (int)(mousePos.Y / pixelEditor.pixelSize);

            for (int x = -brushSize / 2; x <= brushSize / 2; x++)
            {
                for (int y = -brushSize / 2; y <= brushSize / 2; y++)
                {
                    if (rowPos + x >= 0 && rowPos + x < pixelEditor.rows && colPos + y >= 0 && colPos + y < pixelEditor.cols &&
                        (isRectForm || System.MathF.Sqrt((x * x) + (y * y)) <= brushSize / 2))
                    {
                        Color color = brush.Color;
                        float noise = Noise2D.Get(rowPos + x, colPos + y, MainWindow.Settings.octavesNumber, forceBrush * 12, MainWindow.Settings.frequency);
                        color.R = (byte)System.Math.Clamp(color.R - noise, 0, 255);
                        color.G = (byte)System.Math.Clamp(color.G - noise, 0, 255);
                        color.B = (byte)System.Math.Clamp(color.B - noise, 0, 255);
                        SolidColorBrush brushCopy = new SolidColorBrush(color);

                        bool found = false;
                        foreach (UIElement element in pixelEditor.PixelCanvas.Children)
                        {
                            if (element.GetType() == typeof(Rectangle) && Canvas.GetLeft(element) == (rowPos + x) * pixelEditor.pixelSize && Canvas.GetTop(element) == (colPos + y) * pixelEditor.pixelSize)
                            {
                                Rectangle paintedPixel = (Rectangle)element;
                                paintedPixel.Fill = brushCopy;
                                pixelEditor.project.GetLayer(pixelEditor.currentLayer).SetPixel(rowPos + x, colPos + y, new Pixel(brushCopy.Color));
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            Rectangle pixelToPaint = new Rectangle
                            {
                                Width = pixelEditor.pixelSize,
                                Height = pixelEditor.pixelSize,
                                Fill = brushCopy
                            };
                            Canvas.SetLeft(pixelToPaint, (rowPos + x) * pixelEditor.pixelSize);
                            Canvas.SetTop(pixelToPaint, (colPos + y) * pixelEditor.pixelSize);
                            pixelEditor.project.GetLayer(pixelEditor.currentLayer).SetPixel(rowPos + x, colPos + y, new Pixel(brushCopy.Color));
                            pixelEditor.PixelCanvas.Children.Add(pixelToPaint);
                        }
                    }
                }
            }
        }

        public void UseLeftClick(MainWindow mainWindow)
        {
            PaintPixel(mainWindow.PixelEditor, mainWindow.PixelEditor.brush1, mainWindow.ToolBox.GetSliderSize(), mainWindow.ToolBox.IsRectForm(), mainWindow.ToolBox.GetSliderForce());
        }

        public void UseRightClick(MainWindow mainWindow)
        {
            PaintPixel(mainWindow.PixelEditor, mainWindow.PixelEditor.brush2, mainWindow.ToolBox.GetSliderSize(), mainWindow.ToolBox.IsRectForm(), mainWindow.ToolBox.GetSliderForce());
        }
    }
}
