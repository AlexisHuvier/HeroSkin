using HeroSkin.Elements;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace HeroSkin.Utils.Tools
{
    public class Eraser : ITool
    {

        public void ErasePixel(PixelEditor pixelEditor, int brushSize, bool isRectForm)
        {
            Point mousePos = Mouse.GetPosition(pixelEditor.PixelCanvas);
            int rowPos = (int)(mousePos.X / pixelEditor.pixelSize);
            int colPos = (int)(mousePos.Y / pixelEditor.pixelSize);

            for (int x = -brushSize / 2; x <= brushSize / 2; x++)
            {
                for (int y = -brushSize / 2; y <= brushSize / 2; y++)
                {
                    if (rowPos + x >= 0 && rowPos + x < pixelEditor.rows && colPos + y >= 0 && colPos + y < pixelEditor.cols &&
                        (isRectForm || System.MathF.Sqrt(x * x + y * y) <= brushSize / 2))
                    {
                        foreach (UIElement element in pixelEditor.PixelCanvas.Children)
                        {
                            if (element.GetType() == typeof(Rectangle) && Canvas.GetLeft(element) == (rowPos + x) * pixelEditor.pixelSize && Canvas.GetTop(element) == (colPos + y) * pixelEditor.pixelSize)
                            {
                                pixelEditor.project.GetLayer(pixelEditor.currentLayer).SetPixel(rowPos + x, colPos + y, null);
                                pixelEditor.PixelCanvas.Children.Remove(element);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void UseLeftClick(MainWindow mainWindow)
        {
            ErasePixel(mainWindow.PixelEditor, mainWindow.ToolBox.GetSliderSize(), mainWindow.ToolBox.IsRectForm());
        }

        public void UseRightClick(MainWindow mainWindow)
        {
            ErasePixel(mainWindow.PixelEditor, mainWindow.ToolBox.GetSliderSize(), mainWindow.ToolBox.IsRectForm());
        }
    }
}
