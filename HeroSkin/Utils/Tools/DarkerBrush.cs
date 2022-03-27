using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using HeroSkin.Elements;

namespace HeroSkin.Utils.Tools
{
    public class DarkerBrush : ITool
    {

        public void ErasePixel(PixelEditor pixelEditor, int brushSize, bool isRectForm, int forceBrush)
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
                        Color color = pixelEditor.project.GetLayer(pixelEditor.currentLayer).GetPixel(rowPos + x, colPos + y).GetColor();
                        color.R = (byte)System.Math.Clamp(color.R - forceBrush, 0, 255);
                        color.G = (byte)System.Math.Clamp(color.G - forceBrush, 0, 255);
                        color.B = (byte)System.Math.Clamp(color.B - forceBrush, 0, 255);
                        pixelEditor.project.GetLayer(pixelEditor.currentLayer).SetPixel(rowPos + x, colPos + y, new Pixel(color));
                    }
                }
            }
        }

        public void UseLeftClick(MainWindow mainWindow)
        {
            ErasePixel(mainWindow.PixelEditor, mainWindow.ToolBox.GetSliderSize(), mainWindow.ToolBox.IsRectForm(), mainWindow.ToolBox.GetSliderForce());
        }

        public void UseRightClick(MainWindow mainWindow)
        {
            ErasePixel(mainWindow.PixelEditor, mainWindow.ToolBox.GetSliderSize(), mainWindow.ToolBox.IsRectForm(), mainWindow.ToolBox.GetSliderForce());
        }
    }
}
