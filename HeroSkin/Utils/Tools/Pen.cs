using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeroSkin.Utils.Tools
{
    public class Pen : ITool
    {
        public void PaintPixel(Elements.PixelEditor pixelEditor, SolidColorBrush brush)
        {
            Point mousePos = Mouse.GetPosition(pixelEditor.PixelCanvas);
            int rowPos = (int)(mousePos.X / pixelEditor.pixelSize);
            int colPos = (int)(mousePos.Y / pixelEditor.pixelSize);
            double pixelTopPos = colPos * pixelEditor.pixelSize;
            double pixelLeftPos = rowPos * pixelEditor.pixelSize;

            if (Mouse.DirectlyOver.GetType() == typeof(Rectangle))
            {
                Rectangle paintedPixel = (Rectangle)Mouse.DirectlyOver;
                paintedPixel.Fill = brush;
                pixelEditor.project.GetLayer(pixelEditor.currentLayer).SetPixel(rowPos, colPos, new Pixel(brush.Color));
            }
            else
            {
                Rectangle pixelToPaint = new Rectangle
                {
                    Width = pixelEditor.pixelSize,
                    Height = pixelEditor.pixelSize,
                    Fill = brush
                };
                Canvas.SetLeft(pixelToPaint, pixelLeftPos);
                Canvas.SetTop(pixelToPaint, pixelTopPos);
                pixelEditor.project.GetLayer(pixelEditor.currentLayer).SetPixel(rowPos, colPos, new Pixel(brush.Color));
                pixelEditor.PixelCanvas.Children.Add(pixelToPaint);
            }
        }

        public void UseLeftClick(Elements.PixelEditor pixelEditor)
        {
            PaintPixel(pixelEditor, pixelEditor.brush1);
        }

        public void UseRightClick(Elements.PixelEditor pixelEditor)
        {
            PaintPixel(pixelEditor, pixelEditor.brush2);
        }
    }
}
