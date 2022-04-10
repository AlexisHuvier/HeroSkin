using System.Windows;
using System.Text.RegularExpressions;
using System;

namespace HeroSkin.Elements
{
    /// <summary>
    /// Logique d'interaction pour ParametersWindow.xaml
    /// </summary>
    public partial class ParametersWindow : Window
    {
        public ParametersWindow()
        {
            InitializeComponent();

            Is4PxModelCheck.IsChecked = MainWindow.Settings.is4PxModel;
            octavesSlider.Value = MainWindow.Settings.octavesNumber;
            frequencySlider.Value = MainWindow.Settings.frequency * 10;
            saturationSlider.Value = MainWindow.Settings.saturationForce * 100;

            frequencyText.Content = ((int)Math.Round(frequencySlider.Value, 0) / 10f).ToString();
            saturationText.Content = ((int)Math.Round(saturationSlider.Value, 0) / 100f).ToString();
            octavesText.Content = Math.Round(octavesSlider.Value, 0).ToString();

        }

        private void validButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Settings.is4PxModel = Is4PxModelCheck.IsChecked.Value;
            MainWindow.Settings.octavesNumber = Convert.ToInt32(Math.Round(octavesSlider.Value, 0));
            MainWindow.Settings.frequency = (int)Math.Round(frequencySlider.Value, 0) / 10f;
            MainWindow.Settings.saturationForce = (int)Math.Round(saturationSlider.Value, 0) / 100f;
            MainWindow.Settings.Save("settings.json");
            Close();
        }

        private void frequencySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (frequencyText != null)
            {
                frequencyText.Content = ((int)Math.Round(frequencySlider.Value, 0) / 10f).ToString();
            }
        }
        private void saturationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (saturationText != null)
            {
                saturationText.Content = ((int)Math.Round(saturationSlider.Value, 0) / 100f).ToString();
            }
        }

        private void octavesSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (octavesText != null)
            {
                octavesText.Content = Math.Round(octavesSlider.Value, 0).ToString();
            }
        }
    }
}
