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
using OpenTK.Wpf;

namespace HeroSkin.Elements
{
    /// <summary>
    /// Logique d'interaction pour SkinRenderer.xaml
    /// </summary>
    public partial class SkinRenderer : UserControl
    {
        public SkinRenderer()
        {
            InitializeComponent();
            GLWpfControlSettings settings = new GLWpfControlSettings();
            Renderer.Start(settings);
            Utils.Renderer.SkinRendererScene.Ready();
        }

        private void Renderer_Render(TimeSpan obj)
        {
            Utils.Renderer.SkinRendererScene.Render();
        }
    }
}
