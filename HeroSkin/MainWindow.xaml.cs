using System.Windows;
using Microsoft.Win32;

namespace HeroSkin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PixelEditor.SetMainWindow(this);
            ToolBox.SetMainWindow(this);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Title = $"HeroSkin - {openFileDialog.FileName}";
                PixelEditor.OpenFile(openFileDialog.FileName);
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Split(" - ").Length > 1)
            {
                string file = Title.Split(" - ")[1].Replace("*", "");
                Title = $"HeroSkin - {file}";
                PixelEditor.SaveFile(file);
            }
            else
            {
                SaveOn_Click(sender, e);
            }
        }

        private void SaveOn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Image file (*.png; *.jpeg)|*.png;*.jpeg",
            };
            if(saveFileDialog.ShowDialog() == true)
            {
                Title = $"HeroSkin - {saveFileDialog.FileName}";
                PixelEditor.SaveFile(saveFileDialog.FileName);
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
