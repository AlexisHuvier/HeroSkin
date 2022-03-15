using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public int GetSliderSize()
        {
            return (int)Math.Round(SizeSlider.Value, 0);
        }

        public int GetSliderForce()
        {
            return (int)Math.Round(ForceSlider.Value, 0);
        }

        public bool IsRectForm()
        {
            return TypeSelector.SelectedIndex == 0;
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

        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SizeText != null)
            {
                SizeText.Content = GetSliderSize().ToString();
            }
        }

        private void ForceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ForceText != null)
            {
                ForceText.Content = GetSliderForce().ToString();
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

        private void darkerButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = new Utils.Tools.DarkerBrush();
        }

        private void lighterButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = new Utils.Tools.LighterBrush();
        }
    }
}
