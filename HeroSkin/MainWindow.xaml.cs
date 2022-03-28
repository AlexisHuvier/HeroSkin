using System.ComponentModel;
using System.Windows;
using System.IO;
using Microsoft.Win32;

namespace HeroSkin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Utils.Settings Settings;

        public MainWindow()
        {
            Settings = Utils.Settings.FromFile("settings.json");
            Utils.Log.logger.Information("Settings loaded");
            Utils.Log.logger.Debug(Settings.ToString());

            InitializeComponent();
            PixelEditor.SetMainWindow(this);
            ToolBox.SetMainWindow(this);
            Layers.SetMainWindow(this);

            Layers.InitLayers();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Title.EndsWith("*"))
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous sauvegarder le projet avant de quitter ?", "HeroSkin", MessageBoxButton.YesNoCancel);
                switch(result)
                {
                    case MessageBoxResult.Yes:
                        SaveProject_Click(null, null);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            base.OnClosing(e);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (Title.EndsWith("*"))
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous sauvegarder ?", "HeroSkin", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Save_Click(null, null);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }

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
            if (Title.EndsWith("*"))
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous sauvegarder ?", "HeroSkin", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Save_Click(null, null);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
            Title = "HeroSkin";
            PixelEditor.Clear();
        }

        private void Parameters_Click(object sender, RoutedEventArgs e)
        {
            Elements.ParametersWindow win = new Elements.ParametersWindow();
            win.Show();
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SaveProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveOnProject_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
