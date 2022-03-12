using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeroSkin.Utils.Tools
{
    public class Pen : ITool
    {
        public void PaintPixel(Elements.PixelEditor pixelEditor, SolidColorBrush brush, int brushSize)
        {
            Point mousePos = Mouse.GetPosition(pixelEditor.PixelCanvas);
            int rowPos = (int)(mousePos.X / pixelEditor.pixelSize);
            int colPos = (int)(mousePos.Y / pixelEditor.pixelSize);

            for (int x = -brushSize / 2; x <= brushSize / 2; x++)
            {
                for (int y = -brushSize / 2; y <= brushSize / 2; y++)
                {
                    if (rowPos + x >= 0 && rowPos + x < pixelEditor.rows && colPos + y >= 0 && colPos + y < pixelEditor.cols)
                    {
                        bool found = false;
                        foreach (UIElement element in pixelEditor.PixelCanvas.Children)
                        {
                            if (element.GetType() == typeof(Rectangle) && Canvas.GetLeft(element) == (rowPos + x) * pixelEditor.pixelSize && Canvas.GetTop(element) == (colPos + y) * pixelEditor.pixelSize)
                            {
                                Rectangle paintedPixel = (Rectangle)element;
                                paintedPixel.Fill = brush;
                                pixelEditor.project.GetLayer(pixelEditor.currentLayer).SetPixel(rowPos + x, colPos + y, new Pixel(brush.Color));
                                found = true;
                                break;
                            }
                        }

                        if(!found)
                        {
                            Rectangle pixelToPaint = new Rectangle
                            {
                                Width = pixelEditor.pixelSize,
                                Height = pixelEditor.pixelSize,
                                Fill = brush
                            };
                            Canvas.SetLeft(pixelToPaint, (rowPos + x) * pixelEditor.pixelSize);
                            Canvas.SetTop(pixelToPaint, (colPos + y) * pixelEditor.pixelSize);
                            pixelEditor.project.GetLayer(pixelEditor.currentLayer).SetPixel(rowPos + x, colPos + y, new Pixel(brush.Color));
                            pixelEditor.PixelCanvas.Children.Add(pixelToPaint);
                        }
                    }
                }
            }
        }

        public void UseLeftClick(MainWindow mainWindow)
        {
            PaintPixel(mainWindow.PixelEditor, mainWindow.PixelEditor.brush1, mainWindow.ToolBox.GetSliderSize());
        }

        public void UseRightClick(MainWindow mainWindow)
        {
            PaintPixel(mainWindow.PixelEditor, mainWindow.PixelEditor.brush2, mainWindow.ToolBox.GetSliderSize());
        }
    }
}
