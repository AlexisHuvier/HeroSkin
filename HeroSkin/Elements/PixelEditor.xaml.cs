using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using HeroSkin.Utils;

namespace HeroSkin.Elements
{
    /// <summary>
    /// Logique d'interaction pour PixelEditor.xaml
    /// </summary>
    public partial class PixelEditor : UserControl
    {
        public static MainWindow mainWindow;
        public static Project project;
        public int currentLayer = 0;
        public SolidColorBrush currentBrush = new SolidColorBrush(Colors.Black);
        public int rows = 64;
        public int cols = 64;
        public int pixelSize = 10;
        public double canvasHeight = 0;
        public double canvasWidth = 0;

        public PixelEditor()
        {
            InitializeComponent();
            canvasWidth = cols * pixelSize;
            canvasHeight = rows * pixelSize;
            PixelCanvas.Width = canvasWidth;
            PixelCanvas.Height = canvasHeight;
            project = new Project();
            project.AddLayer(rows, cols);
        }

        public Project GetProject()
        {
            return project;
        }

        public void SetMainWindow(MainWindow mainWindow)
        {
            PixelEditor.mainWindow = mainWindow;
        }

        public void SaveFile(string path)
        {
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(rows, cols);
            for(int x = 0; x < rows; x++)
            {
                for(int y = 0; y < cols; y++)
                {
                    Color pixel = project.GetLayer(currentLayer).GetPixel(x, y).GetColor();
                    System.Drawing.Color pixelDrawing = System.Drawing.Color.FromArgb(pixel.A, pixel.R, pixel.G, pixel.B);
                    bitmap.SetPixel(x, y, pixelDrawing);
                }
            }
            bitmap.Save(path);
        }

        public void OpenFile(string path)
        {
            PixelCanvas.Children.Clear();
            InitializeCanvasGridLines();
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(path);
            for(int x = 0; x < rows; x++)
            {
                for(int y = 0; y < cols; y++)
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
                        Width = pixelSize,
                        Height = pixelSize,
                        Fill = new SolidColorBrush(pixelMedia)
                    };
                    Canvas.SetLeft(pixelToPaint, x * pixelSize);
                    Canvas.SetTop(pixelToPaint, y * pixelSize);
                    project.GetLayer(currentLayer).SetPixel(x, y, new Pixel(pixelMedia));
                    PixelCanvas.Children.Add(pixelToPaint);
                }
            }
        }

        private void InitializeCanvasGridLines()
        {
            for(int curRow = 0; curRow <= rows; curRow++)
            {
                Line gridLine = new Line
                {
                    X1 = 0,
                    X2 = canvasWidth,
                    Y1 = pixelSize * curRow,
                    Y2 = pixelSize * curRow,
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = 1
                };
                PixelCanvas.Children.Add(gridLine);
            }

            for(int curCol = 0; curCol <= cols; curCol ++)
            {
                Line gridLine = new Line
                {
                    X1 = pixelSize * curCol,
                    X2 = pixelSize * curCol,
                    Y1 = 0,
                    Y2 = canvasHeight,
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = 1
                };
                PixelCanvas.Children.Add(gridLine);
            }
        }

        private void PaintCurrentPixel()
        {
            if (!mainWindow.Title.EndsWith("*"))
                mainWindow.Title += "*";
            Point mousePos = Mouse.GetPosition(PixelCanvas);
            int rowPos = (int)(mousePos.X / pixelSize);
            int colPos = (int)(mousePos.Y / pixelSize);
            double pixelTopPos = colPos * pixelSize;
            double pixelLeftPos = rowPos * pixelSize;

            if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if(Mouse.DirectlyOver.GetType() == typeof(Rectangle))
                {
                    Rectangle paintedPixel = (Rectangle)Mouse.DirectlyOver;
                    paintedPixel.Fill = currentBrush;
                    project.GetLayer(currentLayer).SetPixel(rowPos, colPos, new Pixel(currentBrush.Color));
                }
                else
                {
                    Rectangle pixelToPaint = new Rectangle
                    {
                        Width = pixelSize,
                        Height = pixelSize,
                        Fill = new SolidColorBrush(Colors.Black)
                    };
                    Canvas.SetLeft(pixelToPaint, pixelLeftPos);
                    Canvas.SetTop(pixelToPaint, pixelTopPos);
                    project.GetLayer(currentLayer).SetPixel(rowPos, colPos, new Pixel(currentBrush.Color));
                    PixelCanvas.Children.Add(pixelToPaint);
                }
            }
            else if(Mouse.RightButton == MouseButtonState.Pressed)
            {
                if(Mouse.DirectlyOver.GetType() == typeof(Rectangle))
                {
                    project.GetLayer(currentLayer).SetPixel(rowPos, colPos, null);
                    PixelCanvas.Children.Remove((UIElement)Mouse.DirectlyOver);
                }
            }
        }

        private void PixelEditor_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeCanvasGridLines();
        }

        private void PixelEditor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PaintCurrentPixel();
        }

        private void PixelEditor_MouseMove(object sender, MouseEventArgs e)
        {
            PaintCurrentPixel();
        }
    }
}
