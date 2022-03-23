
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
            for(int i = 0; i < mainWindow.PixelEditor.project.GetLayerCount(); i ++)
            {
                Layer layer = new Layer();
                layer.SetMainWindow(mainWindow);
                layer.InitLayer(mainWindow.PixelEditor.project.GetLayer(i));
                LayersPanel.Children.Add(layer);
            }
        }

        private void AddLayer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
