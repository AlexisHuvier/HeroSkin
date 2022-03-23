using System.Windows.Controls;
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
            TitleLayer.Content = $"Layer N°{mainWindow.PixelEditor.project.GetLayerID(layer) + 1}";
        }
    }
}
