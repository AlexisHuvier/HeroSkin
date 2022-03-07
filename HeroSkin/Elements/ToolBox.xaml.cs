using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeroSkin.Elements
{
    /// <summary>
    /// Logique d'interaction pour ToolBox.xaml
    /// </summary>
    public partial class ToolBox : UserControl
    {
        public MainWindow mainWindow;
        public Utils.Tools.ITool currentTool;

        public ToolBox()
        {
            InitializeComponent();
            currentTool = new Utils.Tools.Pen();
        }

        public void SetMainWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            cpB1.SelectedColor = Colors.Black;
            cpB2.SelectedColor = Colors.White;
        }

        private void cpB1_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                mainWindow.PixelEditor.brush1 = new SolidColorBrush(e.NewValue.Value);
            }
        }

        private void cpB2_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                mainWindow.PixelEditor.brush2 = new SolidColorBrush(e.NewValue.Value);
            }
        }

        private void penButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = new Utils.Tools.Pen();
        }

        private void eraserButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = new Utils.Tools.Eraser();
        }
    }
}
