
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

        private void AddLayer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
