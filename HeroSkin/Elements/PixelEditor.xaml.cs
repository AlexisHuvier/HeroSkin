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
        public MainWindow mainWindow;
        public Project project;
        public int currentLayer = 0;
        public SolidColorBrush brush1 = new SolidColorBrush(Colors.Black);
        public SolidColorBrush brush2 = new SolidColorBrush(Colors.White);
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
            this.mainWindow = mainWindow;
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
            UpdateBitmap();
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

        private void UpdateBitmap()
        {
            if (!mainWindow.Title.EndsWith("*"))
            {
                mainWindow.Title += "*";
            }

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(rows, cols);
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    Pixel pixel = project.GetLayer(currentLayer).GetPixel(x, y);
                    if (pixel != null)
                    {
                        Color color = pixel.GetColor();
                        System.Drawing.Color pixelDrawing = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
                        bitmap.SetPixel(x, y, pixelDrawing);
                    }
                    else
                        bitmap.SetPixel(x, y, System.Drawing.Color.Transparent);
                }
            }
            Utils.Renderer.SkinRendererScene.SetTexture(bitmap);
        }

        private void UseTool()
        {

            if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                UpdateBitmap();
                mainWindow.ToolBox.currentTool.UseLeftClick(this);
            }
            else if(Mouse.RightButton == MouseButtonState.Pressed)
            {
                UpdateBitmap();
                mainWindow.ToolBox.currentTool.UseRightClick(this);
            }
            
        }

        private void PixelEditor_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeCanvasGridLines();
        }

        private void PixelEditor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UseTool();
        }

        private void PixelEditor_MouseMove(object sender, MouseEventArgs e)
        {
            UseTool();
        }
    }
}
