
using System.Windows;
using System.Windows.Controls;

namespace HeroSkin.Elements
{
    /// <summary>
    /// Logique d'interaction pour Layers.xaml
    /// </summary>
    public partial class Layers : UserControl
    {
        public MainWindow mainWindow;

        public Layers()
        {
            InitializeComponent();
        }

        public void SetMainWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void InitLayers()
        {
            LayersPanel.Children.Clear();
            for(int i = 0; i < mainWindow.PixelEditor.project.GetLayerCount(); i ++)
            {
                Layer layer = new Layer();
                layer.SetMainWindow(mainWindow);
                layer.InitLayer(mainWindow.PixelEditor.project.GetLayer(i));
                LayersPanel.Children.Add(layer);
            }
            mainWindow.PixelEditor.UpdateBitmap();
        }

        private void AddLayer_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.PixelEditor.project.AddLayer(mainWindow.PixelEditor.rows, mainWindow.PixelEditor.cols);
            InitLayers();
        }

        private void DelLayer_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow.PixelEditor.project.GetLayerCount() > 1)
            {
                mainWindow.PixelEditor.project.RemoveLayer(mainWindow.PixelEditor.currentLayer);
                mainWindow.PixelEditor.currentLayer = 0;
                InitLayers();
            }
            else
            {
                _ = MessageBox.Show("Impossible de supprimer le dernier calque.", "HeroSkin", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
