using HeroSkin.Elements;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace HeroSkin.Utils.Tools
{
    public class Eraser : ITool
    {

        public void ErasePixel(PixelEditor pixelEditor)
        {
            Point mousePos = Mouse.GetPosition(pixelEditor.PixelCanvas);
            int rowPos = (int)(mousePos.X / pixelEditor.pixelSize);
            int colPos = (int)(mousePos.Y / pixelEditor.pixelSize);
            double pixelTopPos = colPos * pixelEditor.pixelSize;
            double pixelLeftPos = rowPos * pixelEditor.pixelSize;

            if (Mouse.DirectlyOver.GetType() == typeof(Rectangle))
            {
                pixelEditor.project.GetLayer(pixelEditor.currentLayer).SetPixel(rowPos, colPos, null);
                pixelEditor.PixelCanvas.Children.Remove((UIElement)Mouse.DirectlyOver);
            }
        }

        public void UseLeftClick(PixelEditor pixelEditor)
        {
            ErasePixel(pixelEditor);
        }

        public void UseRightClick(PixelEditor pixelEditor)
        {
            ErasePixel(pixelEditor);
        }
    }
}
