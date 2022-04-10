using System;
using System.Windows;
using System.Windows.Controls;

namespace HeroSkin.Elements
{
    /// <summary>
    /// Logique d'interaction pour SliderPopup.xaml
    /// </summary>
    public partial class SliderPopup : Window
    {
        public int Result => Slider == null ? 0 : Convert.ToInt32(Slider.Value);

        public SliderPopup(string text, int value)
        {
            InitializeComponent();
            TextBox.Text = text;
            Slider.Value = value;
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
