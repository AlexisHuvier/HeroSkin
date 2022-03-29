using System.Windows.Controls;
using System.Windows.Media;

namespace HeroSkin.Elements
{
    /// <summary>
    /// Logique d'interaction pour Layer.xaml
    /// </summary>
    public partial class Layer : UserControl
    {
        public MainWindow mainWindow;
        public Utils.Layer layer;

        public Layer()
        {
            InitializeComponent();
        }

        public void SetMainWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void InitLayer(Utils.Layer layer)
        {
            this.layer = layer;
            int id = mainWindow.PixelEditor.project.GetLayerID(layer);
            TitleLayer.Content = $"Layer N°{id + 1}";
            if (mainWindow.PixelEditor.currentLayer == id)
                BackgroundBorder.Background = new SolidColorBrush(Colors.SlateGray);
            else if (!layer.IsVisible())
                BackgroundBorder.Background = new SolidColorBrush(Colors.DarkGray);

            if (!layer.IsVisible())
                ShowHide.Content = "Ø";

            if (id == 0)
                GoLeft.IsEnabled = false;
            if (id == mainWindow.PixelEditor.project.GetLayerCount() - 1)
                GoRight.IsEnabled = false;
        }

        private void SelectLayer_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            mainWindow.PixelEditor.currentLayer = mainWindow.PixelEditor.project.GetLayerID(layer);
            mainWindow.Layers.InitLayers();
        }

        private void GoLeft_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            int id = mainWindow.PixelEditor.project.GetLayerID(layer);

            if (mainWindow.PixelEditor.currentLayer == id)
                mainWindow.PixelEditor.currentLayer = id - 1;
            else if (mainWindow.PixelEditor.currentLayer == id - 1)
                mainWindow.PixelEditor.currentLayer = id;

            mainWindow.PixelEditor.project.layers[id] = mainWindow.PixelEditor.project.layers[id - 1];
            mainWindow.PixelEditor.project.layers[id - 1] = layer;
            mainWindow.Layers.InitLayers();
        }

        private void ShowHide_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            layer.SetVisible(!layer.IsVisible());
            mainWindow.Layers.InitLayers();
        }

        private void GoRight_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int id = mainWindow.PixelEditor.project.GetLayerID(layer);

            if (mainWindow.PixelEditor.currentLayer == id)
                mainWindow.PixelEditor.currentLayer = id + 1;
            else if (mainWindow.PixelEditor.currentLayer == id + 1)
                mainWindow.PixelEditor.currentLayer = id;

            mainWindow.PixelEditor.project.layers[id] = mainWindow.PixelEditor.project.layers[id + 1];
            mainWindow.PixelEditor.project.layers[id + 1] = layer;
            mainWindow.Layers.InitLayers();
        }
    }
}